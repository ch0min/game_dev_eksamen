using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{ 
    public GameObject deadBody;
    public float health = 100f;
    bool created = false;
    
    
    public void ApplyDamage(float amountDamage) {
        health -= Mathf.Abs(amountDamage);  // Might not need Mathf.Abs.

        if(health <= 0) {
            if(!created) {
                Instantiate(deadBody, transform.position, transform.localRotation);
                created = true;
            }

            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
    
    // void Die() {
    //     rigidBody = GetComponent<Rigidbody>();
    //     rigidBody.AddForce(-transform.forward * enemyForce);
    //     rigidBody.mass = 20;
    //     rigidBody.drag = 0.1f;
    //     rigidBody.freezeRotation = false;
    //
    // }
}
