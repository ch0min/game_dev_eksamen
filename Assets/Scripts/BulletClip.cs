using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletClip : MonoBehaviour
{
    public GameObject bulletClip;
    public Transform bulletClipPosition;
    public float bulletForce = 1000f;


    public float fireRate = 0.5f;
    float nextFiretime = 0f;
    

    void Update() {
        ShootClip();
    }


    void ShootClip() {
        
        if (Input.GetButton("Fire1") && Time.time >= nextFiretime) {
            
            nextFiretime = Time.time + fireRate;

            
            GameObject bulletHolder;
            bulletHolder = Instantiate(bulletClip, transform.position, transform.rotation) as GameObject;
            
            Rigidbody _rigidbody;
            _rigidbody = bulletHolder.GetComponent<Rigidbody>();
            _rigidbody.AddForce(transform.right * bulletForce);
            
            Destroy(bulletHolder, 6.0f);

        }
        
    }
    
}
