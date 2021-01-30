using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkDoFlamengoWeapon : BaseWeapon
{

    [SerializeField] private float _damage = 30f;
    [SerializeField] private float _attackDelay = .1f;
    [SerializeField] private LayerMask _hitMask = default;
    [Header("Space")]
    [SerializeField] private ParticleSystem _fireSystem = default;
    [SerializeField] private GameObject _wallHitEffectPrefab = default;
    [SerializeField] private GameObject _enemyHitEffectPrefab = default;
    [Header("SFX")]
    [SerializeField] private AudioClip _attackSfx = default;

    private Transform _head;

    private AudioSource _audioSource;

    private bool _isAttacking;
    private float _attackTime;

    private float _musicTime;
    private bool _hasPlayedSfx;

    private void Awake()
    {
        _head = GetComponentInParent<Camera>().transform;
        _attackTime = _attackDelay;
        _audioSource = GetComponent<AudioSource>();
        _musicTime = 0f;
    }

    private void OnEnable()
    {
        _audioSource.time = _musicTime;
    }

    private void OnDisable()
    {
        _musicTime = _audioSource.time;
    }

    public override void StartAttack()
    {
        _isAttacking = true;
    }

    public override void StopAttack()
    {
        _isAttacking = false;
    }

    public override void Disable()
    {
        StopAttack();
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
        _fireSystem.Play();
        StartCoroutine(AkAttackSfx());
        RaycastHit hit;
        Debug.DrawRay(_head.position, _head.forward * 100f, Color.red, 10f);
        if (Physics.Raycast(_head.position, _head.forward, out hit, Mathf.Infinity, _hitMask))
        {
            EnemyHealth health = hit.collider.gameObject.GetComponent<EnemyHealth>();
            if (health)
            {
                health.DealDamage(_damage, 6, _head);
                Instantiate(_enemyHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
            }
            else 
            {
                Instantiate(_wallHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
            }

        }
        // Debug.Break();
    }

    public IEnumerator AkAttackSfx()
    {
        _audioSource.PlayOneShot(_attackSfx);
        _hasPlayedSfx = true;
        yield return new WaitForSeconds(0.3f);
        _hasPlayedSfx = false;
    }
}
