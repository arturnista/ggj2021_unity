using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{

    [SerializeField] private float _health = 10f;

    private void OnTriggerEnter(Collider collider)
    {
        PlayerHealth health = collider.GetComponent<PlayerHealth>();
        if (health)
        {
            health.AddHealth(_health);
            Destroy(gameObject);
        }
    }

}
