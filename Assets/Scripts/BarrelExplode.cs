using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplode : MonoBehaviour
{
    Rigidbody rigidBody;
    public float enemyForce = 1000f;
    public GameObject explosionEffect;
    public float blastRadius = 5f;
    public bool exploded = false;
    public LayerMask targetLayerMask = new LayerMask();
    private Material material;

    public void Explode()
    {
        if (!exploded)
        {
            //spawn explode effect
            Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 2.0f);

            //change barrel color
            material = GetComponent<Renderer>().material;
            material.color = Color.gray;

            //kill enemys in blast radius
            Collider[] objectsToExplode = Physics.OverlapSphere(transform.position, blastRadius, targetLayerMask);
            foreach (var o in objectsToExplode)
            {
                if (o.CompareTag("Enemy"))
                {
                    o.gameObject.GetComponent<Enemy>().ApplyDamage(100);
                }

                if (o.CompareTag("Player"))
                {

                }

                //if (o.CompareTag("BarrelHit") && !o.gameObject.GetComponent<BarrelExplode>().exploded)
                // {
                //     o.gameObject.GetComponent<BarrelExplode>().Explode();
                //
                // }
            }

            //make barrel move around
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.AddForce(-transform.forward * enemyForce);
            rigidBody.mass = 20;
            rigidBody.drag = 0.1f;
            rigidBody.freezeRotation = false;

            //todo: change barrel color and/or something else to indicate that it has exploded

            exploded = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
