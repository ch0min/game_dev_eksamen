using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public GameObject bullet;
    public float bulletForce = 2000f;
    public float fireRate = 0.5f;

    public GameObject muzzleFlash;
    public Transform muzzleFlashPosition;
    public float _nextFireTime;

    public AudioSource bulletSFX;
    public AudioSource bulletClipSFX;


    public void Shoot() {
        _nextFireTime = Time.time + fireRate;
        bulletSFX.Play();
        var flash = Instantiate(muzzleFlash, muzzleFlashPosition);

        var bulletHolder = Instantiate(bullet, transform.position, transform.rotation);
        bulletHolder.transform.Rotate(Vector3.left * 90); // Sometimes needed, depends on the rotation of the Weaponholder.

        var _rigidbody = bulletHolder.GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * bulletForce);
        bulletClipSFX.Play();

        Destroy(flash, 0.15f);
        Destroy(bulletHolder, 3.0f);
    }
}
