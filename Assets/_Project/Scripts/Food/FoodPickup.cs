using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{

    [SerializeField] private float _health = 10f;
    [SerializeField] private GameObject _pickUpSfxPlayer = default;

    private void OnTriggerEnter(Collider collider)
    {
        PlayerHealth health = collider.GetComponent<PlayerHealth>();
        if (health)
        {
            Instantiate(_pickUpSfxPlayer, transform.position, Quaternion.identity);
            health.AddHealth(_health);
            Destroy(gameObject);
        }
    }



}
