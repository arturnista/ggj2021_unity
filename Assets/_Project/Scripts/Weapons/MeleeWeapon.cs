using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{

    [Header("Values")]
    [SerializeField] private float _damage = 15f;
    [SerializeField] private int _damageForce = 10;
    [SerializeField] private float _maxAttackTime = 1f;
    [Header("Detection")]
    [SerializeField] private SphereCollider _collider = default;
    [Header("Effects")]
    [SerializeField] private GameObject _impactPrefab = default;
    [SerializeField] private ParticleSystem _chargeEffect = default;
    [SerializeField] private ParticleSystem _halfChargeEffect = default;
    [SerializeField] private ParticleSystem _fullChargeEffect = default;

    private Transform _parent;
    private Animator _animator;

    private bool _isAttacking;
    private float _attackTime;

    private bool _half;
    private bool _full;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _parent = transform.parent;
        _collider.enabled = false;
    }

    public override void StartAttack()
    {
        _animator.SetBool("IsChargingAttack", true);
        _isAttacking = true;
        _attackTime = 0f;
        _chargeEffect.Play();
        _half = false;
        _full = false;
    }

    public override void StopAttack()
    {
        _animator.SetBool("IsChargingAttack", false);
        _isAttacking = false;
        _chargeEffect.Stop();
        _chargeEffect.Clear();
        Attack();
    }
    
    private void Update()
    {
        if (_isAttacking)
        {
            _attackTime += Time.deltaTime;
            if (!_half && _attackTime >= _maxAttackTime / 2f)
            {
                _half = true;
                _halfChargeEffect.Play();
            }
            if (!_full && _attackTime >= _maxAttackTime)
            {
                _full = true;
                _fullChargeEffect.Play();
            }
        }
    }

    private void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        _collider.enabled = true;
        yield return new WaitForSeconds(.2f);
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

            float lerpValue = Mathf.InverseLerp(0f, _maxAttackTime, _attackTime);
            float damage = Mathf.Lerp(0f, _damage, lerpValue);
            int force = Mathf.RoundToInt(Mathf.Lerp(0, _damageForce, lerpValue));

            Instantiate(_impactPrefab, transform.position + transform.forward, Quaternion.identity);
            health.DealDamage(damage, force, transform);
        }
    }
}
