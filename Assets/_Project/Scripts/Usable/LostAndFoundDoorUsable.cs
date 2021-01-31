using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostAndFoundDoorUsable : BaseUsable
{

    public override void Use()
    {
        transform.parent = null;
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.AddForce(transform.up * 10000f);
        Destroy(GetComponent<UnityEngine.AI.NavMeshObstacle>());
        Destroy(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth health = collision.transform.gameObject.GetComponent<EnemyHealth>();
        if (health)
        {
            health.DealDamage(90f, 11, transform);
        }
    }

}
