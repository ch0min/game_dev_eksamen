using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Enemy : MonoBehaviour{ 
    private float health = 100f;
    private Animator anim;

    // [FormerlySerializedAs("_fieldOfView")]
    public AIBehaviour _aiBehaviour;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update() {
        
    }

    public void ApplyDamage(float amountDamage) {
        health -= Mathf.Abs(amountDamage);  // Might not need Mathf.Abs.
        _aiBehaviour.ChasePlayer();
        _aiBehaviour.canSeePlayer = true;

        if(health <= 0) {
            Die();
        }
    }

    public void Die() {
        anim.SetBool("canSeePlayer", false);
        anim.SetBool("attack", false);
        anim.SetTrigger("Death");
        
        Destroy(gameObject, 3f);
    }
}
