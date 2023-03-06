using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public GameObject bullet;
    public float bulletForce = 2000f;
    

    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            GameObject bulletHolder;
            bulletHolder = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            bulletHolder.transform.Rotate(Vector3.left * 90);   // Sometimes needed, depends on the rotation of the Weaponholder.

            Rigidbody _rigidbody;
            _rigidbody = bulletHolder.GetComponent<Rigidbody>();
            _rigidbody.AddForce(transform.forward * bulletForce);
            
            Destroy(bulletHolder, 3.0f);
        }
    }
}
