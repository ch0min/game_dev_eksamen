using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    private float maxHealth = 100f;
    PlayerController player;


    private void Start() {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<PlayerController>();

    }

    private void Update() {
        currentHealth = player.health;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
