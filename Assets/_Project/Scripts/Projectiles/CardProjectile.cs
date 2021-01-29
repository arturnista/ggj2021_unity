using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardProjectile : MonoBehaviour
{
    
    [SerializeField] private float _damage = 10f;
    [SerializeField] private int _damageForce = 2;
    [Space]
    [SerializeField] private GameObject _impactPrefab = default;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Vector3 force = (transform.forward * 1000f) + (transform.up * 150f);
        _rigidbody.AddForce(force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth health = collision.gameObject.GetComponent<EnemyHealth>();
        if (health)
        {
            health.DealDamage(_damage, _damageForce, transform);
        }
        
        Instantiate(_impactPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
