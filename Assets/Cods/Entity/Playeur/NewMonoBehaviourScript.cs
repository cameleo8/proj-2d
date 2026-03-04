using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _run;

    [SerializeField] private float _speed_walk;
    [SerializeField] private float _speed_run;

    [SerializeField] private bool _racer;

    private Vector2 _mouvement;
    private bool _running = false;


    // START


    private void Start()
    {
        if (_move != null)
        {
            _move.action.performed += Deplacement;
            _move.action.canceled += Deplacement;
        }
        if (_run != null)
        {
            _run.action.started += Course;
            _run.action.canceled += Course;

        }

    }

    private void Deplacement(InputAction.CallbackContext ctx)
    {
        _mouvement = ctx.ReadValue<Vector2>().normalized;

    }

    // COURSE

    private void Course(InputAction.CallbackContext ctx)
    {
        if (!_racer)
            _running = !_running;
        else
            _running = ctx.ReadValueAsButton();
    }

    private void Update()
    {
       if(_mouvement.magnitude > 0.1f)
        {
            float speed = _running ? _speed_run : _speed_walk;
            this.transform.Translate(_mouvement*speed*Time.deltaTime);
        }
    }

}

