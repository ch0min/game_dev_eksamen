using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public GameObject deadBody;
    private float health = 100f;
    public Slider healthBar;
    bool created = false;

    // [FormerlySerializedAs("_fieldOfView")]
    public AIBehaviour _aiBehaviour;

    void Update()
    {
        healthBar.value = health;
    }

    public void ApplyDamage(float amountDamage)
    {
        health -= Mathf.Abs(amountDamage);  // Might not need Mathf.Abs.
        _aiBehaviour.ChasePlayer();
        _aiBehaviour.canSeePlayer = true;

        if (health <= 0)
        {
            if (!created)
            {
                Instantiate(deadBody, transform.position, transform.localRotation);
                created = true;
            }

            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
