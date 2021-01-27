using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth = 100f;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void DealDamage(float damage, Transform damager)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
