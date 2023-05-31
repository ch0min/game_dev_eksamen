using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupScript : MonoBehaviour
{
    PlayerController player;
    [SerializeField] int AmmoAmount = 30;

    void Update()
    {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.AddAmmo(AmmoAmount);
        }
    }
}
