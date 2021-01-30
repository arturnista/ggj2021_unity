using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsWeapon : BaseWeapon
{

    [SerializeField] private float _attackDelay = 1f;
    [SerializeField] private GameObject _cardPrefab = default;
    [SerializeField] private AudioClip _cardAttackSfx = default;

    private AudioSource _audioSource;
    private Transform _parent;
    private bool _hasPlayedSfx = false;
    private bool _isAttacking;
    private float _attackTime;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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

    public override void Disable()
    {
        _hasPlayedSfx = false;
        StopAllCoroutines();
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
        if (!_hasPlayedSfx)
        {
            StartCoroutine(PlayAttackSfx());
        }
        Instantiate(_cardPrefab, transform.position, Quaternion.LookRotation(transform.forward, transform.up));
        // Debug.Break();
    }

    IEnumerator PlayAttackSfx()
    {
        _audioSource.PlayOneShot(_cardAttackSfx, 0.6f);
        _hasPlayedSfx = true;
        yield return new WaitForSeconds(0.5f);
        _hasPlayedSfx = false;
    }

}
