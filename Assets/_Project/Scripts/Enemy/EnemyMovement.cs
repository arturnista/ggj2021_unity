using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;

public class EnemyMovement : MonoBehaviour
{

    private GameObject _player;
    private NavMeshAgent _agent;

    private Animator _animator;
    private bool _stopped;
    private EnemyAttack _attack;
    private RangeSensor _sensor;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _attack = GetComponent<EnemyAttack>();
        _sensor = GetComponentInChildren<RangeSensor>();
    }

    private void OnEnable()
    {
        _sensor.OnDetected.AddListener(HandlePlayerDetect);
    }

    private void OnDisable()
    {
        _sensor.OnDetected.RemoveListener(HandlePlayerDetect);
        _agent.isStopped = true;
    }

    private void HandlePlayerDetect(GameObject target, Sensor sensor)
    {
        _player = target;
    }

    private void Update()
    {
        if (_player == null) return;
        if (_agent.isStopped)
        {
            _attack.CancelAttack();
            return;
        }

        if (!_attack.CanMove) return;
        
        _agent.destination = _player.transform.position;
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
        if (Vector3.Distance(_player.transform.position, transform.position) < _agent.stoppingDistance)
        {
            _attack.StartAttacking(_player);
        }
    }

    public void ForceTarget()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
