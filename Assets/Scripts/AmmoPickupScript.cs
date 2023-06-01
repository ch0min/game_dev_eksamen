using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupScript : MonoBehaviour
{
    PlayerController player;
    [SerializeField]
    int AmmoAmount = 30;

    public AudioSource ammoPickupSFX;

    void Update() {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }

    void Awake() {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            ammoPickupSFX.Play();
            Destroy(gameObject, 0.7f);
            player.AddAmmo(AmmoAmount);
        }
    }
}
