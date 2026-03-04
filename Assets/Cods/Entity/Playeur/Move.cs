using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Move : MonoBehaviour
{
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _run;

    [SerializeField] private float _speed_w;
    [SerializeField] private float _speed_r;


    private bool _running = false;
    private Vector2 _deplacement;

    private Coroutine _moveCoroutine;

    void Start()
    {
        if (_move  != null)
        {
            _move.action.performed += Deplacement;
            _move.action.canceled += Deplacement;
        }
        if (_run != null)
        {
            if (_moveCoroutine == null)
                _moveCoroutine = StartCoroutine(MoveCoroutine());
        }
        else
        {
            StopDeplacement();
        }
    }

    private void Deplacement(InputAction.CallbackContext ctx)
    {
        _deplacement = ctx.ReadValue<Vector2>().normalized;

        if (_deplacement.magnitude > 0.1f){

        }
    }

    private void StopDeplacement(InputAction.CallbackContext ctx = default)
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while (_deplacement.magnitude > 0.1f)
        {
            float speed = _running ? _speed_r : _speed_w;
            transform.Translate(_deplacement.normalized * speed * Time.deltaTime);
            yield return null; // attend la prochaine frame
        }
        _moveCoroutine = null; // coroutine terminée
    }



    private void Coursse(InputAction.CallbackContext ctx)
    {
        _running = !_running;
    }


    void Update()
    {
        
    }
}
