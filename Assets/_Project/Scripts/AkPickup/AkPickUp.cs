using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkPickUp : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider collider)
    {
        PlayerAttack attack = collider.gameObject.GetComponent<PlayerAttack>();
        if (attack)
        {
            attack.ActiveWeapon(3);
            Destroy(gameObject);
        }
    }

}
