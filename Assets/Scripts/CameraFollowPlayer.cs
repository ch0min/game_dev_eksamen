using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour{
    public Transform followPlayer;
    private Transform cameraTransform;

    // Defining the position of the Camera
    public Vector3 playerOffset;
    
    // Speed of the Camera
    public float camSpeed = 400f;
    
    void Start() {
        cameraTransform = transform;
    }

    void SetTarget(Transform newTransformTarget) {      // Updates player transform
        followPlayer = newTransformTarget;
    }

    void LateUpdate() {
        if(followPlayer != null) {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, followPlayer.position + playerOffset,
                camSpeed * Time.deltaTime);
        }
        
    }
}
