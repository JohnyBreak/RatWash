using UnityEngine.AI;
using UnityEngine;
using System;
using Zenject;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private NavMeshAgent _agent;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {

        _anim.SetFloat("Walk",_joystick.Direction.magnitude);

        if (_joystick.Direction == Vector2.zero) return;
        Vector3 movement = _agent.speed * Time.deltaTime * new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);

        transform.LookAt(transform.position + movement, Vector3.up);
        _agent.Move(movement);
    }

}
