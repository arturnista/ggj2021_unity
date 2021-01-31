using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorWeapon : BaseWeapon
{

    [SerializeField] private float _damage = 6f;
    [SerializeField] private float _attackDelay = 0.5f;
    [SerializeField] private LayerMask _hitMask = default;
    [Header("Effects")]
    [SerializeField] private GameObject _wallHitEffectPrefab = default;
    [SerializeField] private GameObject _enemyHitEffectPrefab = default;
    [Header("Sounds")]
    [SerializeField] private AudioClip _attackSfx = default;
    [Header("Projectile")]
    [SerializeField] private Transform _firePoint = default;
    [SerializeField] private GameObject _projectilePrefab = default;
    private AudioSource _audioSource;
    private Transform _head;
    private bool _isAttacking;
    private float _attackTime;
    private bool _hasPlayedSfx = false;
    private Animator _animator;

    private void Awake() 
    {
        _head = GetComponentInParent<Camera>().transform;
        _audioSource = GetComponent<AudioSource>();  
        _animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        _attackTime += Time.deltaTime;
        if (_isAttacking && _attackTime > _attackDelay)
        {
            _attackTime = 0f;
            if (!_hasPlayedSfx)
            {
                StartCoroutine(AkAttackSfx());
            }

            for (int i = 0; i < 9; i++)
            {
                Attack();
            }
        }    
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
        _hasPlayedSfx = false;
        StopAllCoroutines();
        StopAttack();
    }

    private void Attack()
    {
        _animator.SetTrigger("Fire");
        Vector3 direction = _head.forward;
        Vector3 spread = Vector3.zero;

        spread += _head.up * Random.Range(-1f, 1f);
        spread += _head.right * Random.Range(-1f, 1f);

        direction += spread.normalized * Random.Range(0.02f, 0.1f);

        RaycastHit hit;
        Debug.DrawRay(_head.position, direction * 100f, Color.red, 10f);

        if (Physics.Raycast(_head.position, direction, out hit, Mathf.Infinity, _hitMask))
        {
            EnemyHealth health = hit.collider.gameObject.GetComponent<EnemyHealth>();
            if (health)
            {
                health.DealDamage(_damage, 2, _head);
                Instantiate(_enemyHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
            }
            else 
            {
                Instantiate(_wallHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
            }
            StartCoroutine(TrailCoroutine(hit));
        }

    }

    private IEnumerator TrailCoroutine(RaycastHit hit)
    {
        GameObject created = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        float time = 0f;
        while (time < .1f)
        {
            time += Time.deltaTime;
            created.transform.position = Vector3.Lerp(_firePoint.position, hit.point, time / .1f);
            yield return null;
        }
    }

    public IEnumerator AkAttackSfx()
    {
        _audioSource.PlayOneShot(_attackSfx, 0.7f);
        _hasPlayedSfx = true;
        yield return new WaitForSeconds(0.9f);
        _hasPlayedSfx = false;
    }

}
