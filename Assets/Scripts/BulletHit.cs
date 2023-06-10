using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    // particle effects.
    public GameObject bloodEffect;
    public GameObject hitEffect;
    
    public float damage = 25f;

    void OnCollisionEnter(Collision collision) {
        // tager fat i contactpoint for collision, hvor to objects collider med hinanden.
        ContactPoint contact = collision.contacts[0];
        if (collision.gameObject.CompareTag("Enemy")) {
            Enemy target = collision.transform.gameObject.GetComponent<Enemy>();
            Instantiate(bloodEffect, contact.point, Quaternion.LookRotation(contact.normal));
            target.ApplyDamage(damage);
        }
        else if (collision.gameObject.CompareTag("WallHit")) {
            Instantiate(hitEffect, contact.point, Quaternion.LookRotation(contact.normal));
        }
        else if (collision.gameObject.CompareTag("BarrelHit")) {
            BarrelExplode target = collision.transform.gameObject.GetComponent<BarrelExplode>();
            target.Explode();
        }

        Destroy(gameObject);
    }
}
