using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody rigidBody;
    Camera mainCamera;
    public static Vector3 playerPos;

    
    public float moveSpeed = 4;
    
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();

        StartCoroutine(TrackPlayer());
    }

    IEnumerator TrackPlayer() {
        while (true) {
            playerPos = gameObject.transform.position;
            yield return null;
        }
    }
     

    void FixedUpdate() {
        
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        // rigidBody.AddForce(movement * moveSpeed / Time.deltaTime);



        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        
        rigidBody.AddForce(Vector3.ClampMagnitude(movement, 1) * moveSpeed / Time.deltaTime);


        // Face Mouse
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        
        if(groundPlane.Raycast(cameraRay, out rayLength)) {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

    }
}
