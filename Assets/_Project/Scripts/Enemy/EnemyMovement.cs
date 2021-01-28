using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private GameObject _player;
    private NavMeshAgent _agent;

    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnDisable()
    {
        _agent.isStopped = true;
    }

    private void Update()
    {
        _agent.destination = _player.transform.position;
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    public void Pause()
    {
        _agent.isStopped = true;
    }

    public void Resume()
    {
        _agent.isStopped = false;
    }

}
