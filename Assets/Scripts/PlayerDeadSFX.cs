using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerDeadSFX : MonoBehaviour
{
    public AudioSource playerDeadSFX;

    void Start() {
        playerDeadSFX.Play();
    }
}
