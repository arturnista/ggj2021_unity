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
    private Coroutine _idleCoroutine;
    private Vector3 _originalPosition;
    private float _originalSpeed;

    private void Awake()
    {
        _state = GetComponent<EnemyState>();
        _attack = GetComponent<EnemyAttack>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _sensor = GetComponentInChildren<RangeSensor>();

        _source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (_state.CurrentState == EnemyState.State.Idle)
        {
            _originalPosition = transform.position;
            _originalSpeed = _agent.speed;
            _idleCoroutine = StartCoroutine(IdleMoveCoroutine());
            _agent.speed = 1f;
        }
    }

    private IEnumerator IdleMoveCoroutine()
    {
        while (true)
        {
            
            Vector2 randomness = Random.insideUnitCircle * 15f;
            _agent.destination = _originalPosition + new Vector3(randomness.x, 0f, randomness.y);

            yield return new WaitForSeconds(Random.Range(5f, 10f));

        }
    }

    private void OnEnable()
    {
        _sensor.OnDetected.AddListener(HandlePlayerDetect);
        _state.OnChangeState += HandleChangeState;
    }

    private void OnDisable()
    {
        _sensor.OnDetected.RemoveListener(HandlePlayerDetect);
        _state.OnChangeState -= HandleChangeState;
        _agent.isStopped = true;
    }

    private void HandleChangeState(EnemyState.State newState, EnemyState.State lastState)
    {
        if (lastState == EnemyState.State.Idle)
        {
            if (_idleCoroutine != null) StopCoroutine(_idleCoroutine);
            _agent.destination = transform.position;
            if (_originalSpeed > 0) _agent.speed = _originalSpeed;
        }
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
        if (Vector3.Distance(_player.transform.position, transform.position) < _agent.stoppingDistance)
        {
            _attack.StartAttacking(_player);
        }
    }

    private void LateUpdate()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    public void ForceTarget()
    {
        SetTarget(GameObject.FindGameObjectWithTag("Player"));
    }

    private void SetTarget(GameObject target)
    {
        if (_player != null) return;
        _player = target;

        _source.PlayOneShot(_screamClip);
        if (Random.value > .15f) StartCoroutine(ScreamCoroutine());
        else _state.ChangeState(EnemyState.State.Moving); 
    }

    private IEnumerator ScreamCoroutine()
    {
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
