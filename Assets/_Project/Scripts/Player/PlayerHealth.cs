using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth = 100f;

    private Animator _animator;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void DealDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.LogFormat("Health: {0}", _currentHealth);
    }

}
