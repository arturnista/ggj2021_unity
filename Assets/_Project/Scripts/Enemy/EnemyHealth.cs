using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth = 100f;
    
    private EnemyState _state;
    private EnemyMovement _enemyMovement;

    private Animator _animator;
    private bool _isKnockedOut;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        _state = GetComponent<EnemyState>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    public void DealDamage(float damage, int damageForce, Transform damager)
    {
        _currentHealth -= damage;
        _enemyMovement.ForceTarget();
        
        if (_currentHealth <= 0f)
        {
            _state.ChangeState(EnemyState.State.Dying);
            _animator.SetTrigger("Death");
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

        int force = Mathf.Clamp(damageForce, 0, 10);

        _enemyMovement.Pause();
        
        if (force > 5 && force > Random.Range(0, 14))
        {

            _state.ChangeState(EnemyState.State.Knockout);
            _animator.SetTrigger("Knockout");
            yield return new WaitForSeconds(4f);
            _state.ChangeState(EnemyState.State.Moving);

        }
        else
        {

            float lerpValue = Mathf.InverseLerp(0, 10, damageForce);
            float time = Mathf.Lerp(0f, 1.5f, lerpValue);
            yield return new WaitForSeconds(time);

        }
        
        _enemyMovement.Resume();
        
    }

}
