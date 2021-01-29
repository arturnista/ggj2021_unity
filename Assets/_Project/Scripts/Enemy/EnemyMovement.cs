using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private AudioClip _screamClip = default;

    private GameObject _player;
    private NavMeshAgent _agent;

    private EnemyState _state;
    private EnemyAttack _attack;
    private Animator _animator;
    private bool _stopped;
    private RangeSensor _sensor;

    private AudioSource _source;

    private void Awake()
    {
        _state = GetComponent<EnemyState>();
        _attack = GetComponent<EnemyAttack>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _sensor = GetComponentInChildren<RangeSensor>();

        _source = GetComponent<AudioSource>();
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
        SetTarget(target);
    }

    private void Update()
    {
        if (_player == null) return;
        if (_state.CurrentState != EnemyState.State.Moving) return;
        
        _agent.destination = _player.transform.position;
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
        if (Vector3.Distance(_player.transform.position, transform.position) < _agent.stoppingDistance)
        {
            _attack.StartAttacking(_player);
        }
    }

    public void ForceTarget()
    {
        SetTarget(GameObject.FindGameObjectWithTag("Player"));
    }

    private void SetTarget(GameObject target)
    {
        if (_player != null) return;
        _player = target;

        if (Random.value > .3f) StartCoroutine(ScreamCoroutine());
        else _state.ChangeState(EnemyState.State.Moving); 
    }

    private IEnumerator ScreamCoroutine()
    {
        _source.PlayOneShot(_screamClip);
        _animator.SetTrigger("Scream");
        _state.ChangeState(EnemyState.State.Scream);
        yield return new WaitForSeconds(2f);
        _state.ChangeState(EnemyState.State.Moving);
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
