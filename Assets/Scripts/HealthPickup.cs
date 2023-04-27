using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerController player;
    [SerializeField] float healthBonus = 10f;


    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
        player.ModifyHealth(healthBonus);
    }
}
