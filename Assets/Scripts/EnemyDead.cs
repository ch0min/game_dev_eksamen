using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDead : MonoBehaviour{
    Rigidbody rigidBody;
    public float enemyForce = 1000f;
    
    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(-transform.forward * enemyForce);
    }


}
