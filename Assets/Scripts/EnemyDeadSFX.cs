using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDeadSFX : MonoBehaviour
{
    public AudioSource enemyDeadSFX;
    

    void Start() {
        enemyDeadSFX.Play();
    }
}
