using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{

    [SerializeField] private float _damage = 15f;
    [SerializeField] private float _damageForce = 15f;
    [SerializeField] private float _attackDelay = 1f;
    [SerializeField] private SphereCollider _collider = default;
    [SerializeField] private GameObject _impactPrefab = default;

    private Transform _parent;
    private Animator _animator;

    private bool _isAttacking;
    private float _attackTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _parent = transform.parent;
        _attackTime = _attackDelay;
        _collider.enabled = false;
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
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        _collider.enabled = true;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(_attackDelay - .1f);
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        EnemyHealth health = collider.gameObject.GetComponent<EnemyHealth>();
        if (health)
        {
            // RaycastHit hit;
            // if (Physics.Raycast(transform.position, transform.forward, out hit))
            // {
            //     Instantiate(_impactPrefab, hit.point, Quaternion.identity);
            // }

            Instantiate(_impactPrefab, transform.position + transform.forward, Quaternion.identity);
            health.DealDamage(_damage, _damageForce, transform);
        }
    }
}
