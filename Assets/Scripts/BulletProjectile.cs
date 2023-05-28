using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public GameObject bullet;
    public float bulletForce = 2000f;
    public float fireRate = 0.5f;
    public int reserveAmmo;

    public GameObject muzzleFlash;
    public Transform muzzleFlashPosition;
    private float _nextFireTime;

    private void Update()
    {
        // Shoot();
    }

    public void Shoot()
    {
        // if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        if (Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + fireRate;
            var flash = Instantiate(muzzleFlash, muzzleFlashPosition);

            var bulletHolder = Instantiate(bullet, transform.position, transform.rotation);
            bulletHolder.transform.Rotate(Vector3.left *
                                          90); // Sometimes needed, depends on the rotation of the Weaponholder.

            var _rigidbody = bulletHolder.GetComponent<Rigidbody>();
            _rigidbody.AddForce(transform.forward * bulletForce);

            Destroy(flash, 0.15f);
            Destroy(bulletHolder, 3.0f);
        }
    }
}