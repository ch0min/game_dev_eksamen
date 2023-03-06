using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour{
    public GameObject bloodEffect;
    public GameObject hitEffect;
    public float damage = 25f;

    void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.contacts[0];
        if(collision.gameObject.tag == "Enemy" ) {
            Enemy target = collision.transform.gameObject.GetComponent<Enemy>();
            Instantiate(bloodEffect, contact.point, Quaternion.LookRotation(contact.normal));
            target.ApplyDamage(damage);
        } else if(collision.gameObject.tag == "WallHit") {
            Instantiate(hitEffect, contact.point, Quaternion.LookRotation(contact.normal));
        }
        
        Destroy(gameObject);
    }
}
