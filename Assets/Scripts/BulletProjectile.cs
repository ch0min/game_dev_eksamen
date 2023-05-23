using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public GameObject bullet;
    public float bulletForce = 2000f;
    public float fireRate = 0.5f;
    float nextFiretime = 0f;


    public GameObject muzzleFlash;
    public Transform muzzleFlashPosition;

    void Update() {
        Shoot();
    }

    void Shoot() {
        if (Input.GetButton("Fire1") && Time.time >= nextFiretime) {
            nextFiretime = Time.time + fireRate;
            GameObject Flash = Instantiate(muzzleFlash, muzzleFlashPosition);
            
            GameObject bulletHolder;
            bulletHolder = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            bulletHolder.transform.Rotate(Vector3.left * 90); // Sometimes needed, depends on the rotation of the Weaponholder.
            
            Rigidbody _rigidbody;
            _rigidbody = bulletHolder.GetComponent<Rigidbody>();
            _rigidbody.AddForce(transform.forward * bulletForce);
            
            Destroy(Flash, 0.15f);
            Destroy(bulletHolder, 3.0f);

        }
    }
}
