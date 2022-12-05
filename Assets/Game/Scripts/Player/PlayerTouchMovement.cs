using UnityEngine.AI;
using UnityEngine;
using System;
using Zenject;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private NavMeshAgent _agent;
    private BuyCanvas _buyCanvas;
    private Animator _anim;
    private bool _canMove = true;

    [Inject]
    private void Construct(BuyCanvas buyCanvas)
    {
        _buyCanvas = buyCanvas;
    }

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _buyCanvas.OpenScreenEvent += OnStopPlayer;
        _buyCanvas.HideScreenEvent += OnResumePlayer;
    }

    private void OnStopPlayer() 
    {
        _canMove = false;
    }

    private void OnResumePlayer()
    {
        _canMove = true;
    }


    private void Update()
    {
        

        _anim.SetFloat("Walk",_joystick.Direction.magnitude);
        if (!_canMove) return;
        if (_joystick.Direction == Vector2.zero) return;
        Vector3 movement = _agent.speed * Time.deltaTime * new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);

        transform.LookAt(transform.position + movement, Vector3.up);
        _agent.Move(movement);
    }


    private void OnDestroy()
    {
        _buyCanvas.OpenScreenEvent -= OnStopPlayer;
        _buyCanvas.HideScreenEvent -= OnResumePlayer;
    }
}
