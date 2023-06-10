using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    // bullet
    public GameObject bullet;
    public float bulletForce = 2000f;

    // muzzleflash
    public GameObject muzzleFlash;
    public Transform muzzleFlashPosition;
    
    // fire cooldown
    public float fireRate = 0.5f;
    public float _nextFireTime;

    public AudioSource bulletSFX;
    public AudioSource bulletClipSFX;


    public void Shoot() {
        // bullet vil blive instantiated hvert 0.5s frame.
        _nextFireTime = Time.time + fireRate;
        bulletSFX.Play();
        var flash = Instantiate(muzzleFlash, muzzleFlashPosition);

        var bulletHolder = Instantiate(bullet, transform.position, transform.rotation);
        // bulletHolder.transform.Rotate(Vector3.left * 90); // måske brugbart, i forohld til rotation af våben.

        var _rigidbody = bulletHolder.GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * bulletForce);
        bulletClipSFX.Play();

        // siden det er en prefab animation.
        Destroy(flash, 0.15f);
        Destroy(bulletHolder, 3.0f);
    }
}
