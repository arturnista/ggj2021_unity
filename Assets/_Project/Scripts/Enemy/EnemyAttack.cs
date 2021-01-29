using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    
    [SerializeField] private float _damage = 5f;
    
    private EnemyState _state;
    private Animator _animator;
    private NavMeshAgent _agent;

    private GameObject _target;
    
    private void Awake()
    {
        _state = GetComponent<EnemyState>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        _state.OnChangeState += HandleChangeState;
    }

    private void Update()
    {
        if (_state.CurrentState != EnemyState.State.Attacking) return;
        if (Vector3.Distance(_target.transform.position, transform.position) > _agent.stoppingDistance)
        {
            StopAttacking();
        }
    }

    private void HandleChangeState(EnemyState.State newState, EnemyState.State lastState)
    {
        if (lastState == EnemyState.State.Attacking)
        {
            _animator.SetBool("IsAttacking", false);
        }
    }

    public void StartAttacking(GameObject target)
    {
        _animator.SetBool("IsAttacking", true);
        _state.ChangeState(EnemyState.State.Attacking);
        _target = target;
    }

    private void StopAttacking()
    {
        _animator.SetBool("IsAttacking", false);
        _state.ChangeState(EnemyState.State.StopAttacking);
        StartCoroutine(FailsafeCoroutine());
    }
    
    private void _AttackContact()
    {
        if (Vector3.Distance(_target.transform.position, transform.position) < _agent.stoppingDistance)
        {
            PlayerHealth health = _target.GetComponent<PlayerHealth>();
            if (health)
            {
                health.DealDamage(_damage);
            }
        }
    }
    
    private void _EndAttack()
    {
        if (_state.CurrentState == EnemyState.State.StopAttacking)
        {
            StopAllCoroutines();
            _state.ChangeState(EnemyState.State.Moving);
        }
    }

    private IEnumerator FailsafeCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _state.ChangeState(EnemyState.State.Moving);
    }

}
