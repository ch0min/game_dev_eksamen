using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource soundTriggerSFX;

    public GameObject _enemy;

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Player") && _enemy != null) {
            soundTriggerSFX.Play();
        }
    }

    void Update() {
        if (_enemy == null) {
            soundTriggerSFX.Stop();
        }
    }
}
