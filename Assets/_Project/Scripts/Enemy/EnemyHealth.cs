using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private AudioClip _damageSfx = default;
    [SerializeField] private AudioClip _deathSfx = default;
    private float _currentHealth = 100f;
    
    private EnemyState _state;
    private EnemyMovement _enemyMovement;

    private Animator _animator;
    private bool _isKnockedOut;

    private AudioSource _audioSource;
    private bool _hasPlayedDamageSfx = false;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        _state = GetComponent<EnemyState>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void DealDamage(float damage, int damageForce, Transform damager)
    {
        _currentHealth -= damage;
        _enemyMovement.ForceTarget();
        
        if (_currentHealth <= 0f)
        {
            _state.ChangeState(EnemyState.State.Dying);
            _audioSource.PlayOneShot(_deathSfx);
            _animator.SetTrigger("Death");
            gameObject.tag = "Untagged";
            Destroy(GetComponent<EnemyAttack>());
            Destroy(GetComponent<SensorToolkit.RangeSensor>());
            Destroy(GetComponent<Collider>());
            Destroy(_enemyMovement);
            Destroy(this);
        }
        else
        {
            if (_state.CurrentState != EnemyState.State.Knockout)
            {
                StopAllCoroutines();
                StartCoroutine(TakeDamageCoroutine(damageForce));
            }
        }
    }

    private IEnumerator TakeDamageCoroutine(int damageForce)
    {

        int force = Mathf.Clamp(damageForce, 0, 11);

        _enemyMovement.Pause();
        
        if (!_hasPlayedDamageSfx)
        {
            StartCoroutine(DamageSfxCoroutine());
        }
        
        if (force == 11)
        {
            yield return StartCoroutine(KnockoutCoroutine());
        }
        else if (force > 5 && force > Random.Range(0, 14))
        {
            yield return StartCoroutine(KnockoutCoroutine());
        }
        else
        {

            float lerpValue = Mathf.InverseLerp(0, 10, damageForce);
            float time = Mathf.Lerp(0f, 1.5f, lerpValue);
            yield return new WaitForSeconds(time);

        }
        
        _enemyMovement.Resume();
        
    }

    private IEnumerator KnockoutCoroutine()
    {
        _state.ChangeState(EnemyState.State.Knockout);
        _animator.SetTrigger("Knockout");
        yield return new WaitForSeconds(4f);
        _state.ChangeState(EnemyState.State.Moving);
    }

    private IEnumerator DamageSfxCoroutine()
    {
        _audioSource.PlayOneShot(_damageSfx, 0.7f);
        _hasPlayedDamageSfx = true;
        yield return new WaitForSeconds(0.5f);
        _hasPlayedDamageSfx = false;
    }

}
