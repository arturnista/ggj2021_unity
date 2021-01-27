using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsWeapon : BaseWeapon
{

    [SerializeField] private float _attackDelay = 1f;
    [SerializeField] private GameObject _cardPrefab = default;

    private Transform _parent;

    private bool _isAttacking;
    private float _attackTime;

    private void Awake()
    {
        _parent = transform.parent;
        _attackTime = _attackDelay;
    }

    public override void StartAttack()
    {
        _isAttacking = true;
    }

    public override void StopAttack()
    {
        _isAttacking = false;
    }
    
    private void Update()
    {
        _attackTime += Time.deltaTime;
        if (_isAttacking && _attackTime > _attackDelay)
        {
            _attackTime = 0f;
            Attack();
        }
    }

    private void Attack()
    {
        Instantiate(_cardPrefab, transform.position, Quaternion.LookRotation(transform.forward, transform.up));
        // Debug.Break();
    }

}
