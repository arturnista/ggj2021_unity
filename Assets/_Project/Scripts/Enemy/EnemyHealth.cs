using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth = 100f;
    private EnemyMovement _enemyMovement;

    private Animator _animator;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    public void DealDamage(float damage, float damageForce, Transform damager)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(TakeDamageCoroutine(damageForce));
        }
    }

    private IEnumerator TakeDamageCoroutine(float damageForce)
    {
        _enemyMovement.Pause();
        if (damageForce < 15f) {
            float lerpValue = Mathf.InverseLerp(0f, 15f, damageForce);
            float time = Mathf.Lerp(0f, 1.5f, lerpValue);
            yield return new WaitForSeconds(time);
        } else {
            _animator.SetTrigger("Knockout");
            yield return new WaitForSeconds(3f);
        }
        _enemyMovement.Resume();
    }

}
