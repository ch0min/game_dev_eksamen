using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody playerRB;

    void Start() {

    }


    void Update() {
        playerRB = GetComponent<Rigidbody>();
        if(Input.GetKey("w")) {
            playerRB.AddForce(Vector3.forward);
        }
        if(Input.GetKey("a")) {
            playerRB.AddForce(Vector3.left);
        }
        if(Input.GetKey("s")) {
            playerRB.AddForce(Vector3.back);
        }
        if(Input.GetKey("d")) {
            playerRB.AddForce(Vector3.right);
        }
    }
}
