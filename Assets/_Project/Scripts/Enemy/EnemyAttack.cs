using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{

    private enum State
    {
        NotAttacking,
        Attacking,
        StopAttacking
    }
    
    [SerializeField] private float _damage = 5f;
    
    private Animator _animator;
    private NavMeshAgent _agent;
    private State _state;

    private GameObject _target;

    public bool CanMove => _state == State.NotAttacking;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _state = State.NotAttacking;
    }

    private void Update()
    {
        if (_state != State.Attacking) return;
        if (Vector3.Distance(_target.transform.position, transform.position) > _agent.stoppingDistance)
        {
            StopAttacking();
        }
    }

    public void StartAttacking(GameObject target)
    {
        _animator.SetBool("IsAttacking", true);
        _state = State.Attacking;
        _target = target;
    }

    private void StopAttacking()
    {
        _animator.SetBool("IsAttacking", false);
        _state = State.StopAttacking;
    }

    public void CancelAttack()
    {
        _state = State.NotAttacking;
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
        if (_state == State.StopAttacking)
        {
            _state = State.NotAttacking;
        }
    }

}
