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
    [Header("SFX")]
    [SerializeField] private AudioClip[] _meleeAttackSfx = default;
    [SerializeField] private AudioClip _bulkDestroySfx = default;

    private Transform _parent;
    private Animator _animator;

    private bool _isAttacking;
    private float _attackTime;

    private bool _halfEffect;
    private bool _fullEffect;

    private AudioSource _audioSource;
    private bool _hasPlayedSfx = false;
    private int _attackCounter;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();

        _parent = transform.parent;
        _collider.enabled = false;
    }

    public override void StartAttack()
    {
        _animator.SetBool("IsChargingAttack", true);
        _isAttacking = true;
        _attackTime = 0f;
        _chargeEffect.Play();
        _halfEffect = false;
        _fullEffect = false;
    }

    public override void StopAttack()
    {
        _animator.SetBool("IsChargingAttack", false);
        _isAttacking = false;
        _chargeEffect.Stop();
        _chargeEffect.Clear();
        Attack();
    }

    public override void Disable()
    {
        _collider.enabled = false;
        StopAllCoroutines();
    }
    
    private void Update()
    {
        if (_isAttacking)
        {
            _attackTime += Time.deltaTime;
            if (!_halfEffect && _attackTime >= _maxAttackTime / 2f)
            {
                _halfEffect = true;
                _halfChargeEffect.Play();
            }
            if (!_fullEffect && _attackTime >= _maxAttackTime)
            {
                _fullEffect = true;
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
        if (!_hasPlayedSfx)
        {
            Debug.Log("entrou");
            _audioSource.PlayOneShot(GetRandomAttackClip(), 0.5f);
            _hasPlayedSfx = true;
        }
        _attackCounter++;

        yield return new WaitForSeconds(.2f);

        if (_attackCounter >= 4)
        {
            _audioSource.PlayOneShot(_bulkDestroySfx);
            _attackCounter = 0;
        }

        _hasPlayedSfx = false;
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

    AudioClip GetRandomAttackClip()
    {
        return _meleeAttackSfx[Random.Range(0, _meleeAttackSfx.Length)];
    }
}
