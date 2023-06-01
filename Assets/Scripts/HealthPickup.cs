using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerController player;
    [SerializeField]
    float healthBonus = 10f;
    
    public AudioSource healthPickupSFX;

    void Update() {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }

    void Awake() {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            healthPickupSFX.Play();
            Destroy(gameObject, 0.7f);
            player.ModifyHealth(healthBonus);
        }
    }
}
