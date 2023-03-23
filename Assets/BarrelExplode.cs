using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplode : MonoBehaviour
{
    Rigidbody rigidBody;
    public float enemyForce = 1000f;

    public GameObject explosionEffect;
    public float blastRadius = 100f;
    public LayerMask targetLayerMask = new LayerMask();
    bool exploded = false;

    public void Explode()
    {
        if (!exploded)
        {
            var colliders = Physics.OverlapSphere(transform.position, blastRadius, targetLayerMask);

            foreach (Collider col in colliders)
            {
                // Enemy target = col.transform.gameObject.GetComponent<Enemy>();
                //
                // Instantiate(bloodEffect, contact.point, Quaternion.LookRotation(contact.normal));
                // target.ApplyDamage(100);
            }
            Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 2.0f);

            rigidBody = GetComponent<Rigidbody>();
            rigidBody.AddForce(-transform.forward * enemyForce);
            rigidBody.mass = 20;
            rigidBody.drag = 0.1f;
            rigidBody.freezeRotation = false;
            exploded = true;
        }
    }
}
