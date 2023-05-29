using UnityEngine;

public class BulletClip : MonoBehaviour
{
    public GameObject bulletClip;
    public Transform bulletClipPosition;
    public float bulletForce = 1000f;

    public float fireRate = 0.5f;
    private float nextFiretime;


    private void Update()
    {
        // ShootClip();
    }


    public void ShootClip()
    {
        // if (Input.GetButton("Fire1") && Time.time >= nextFiretime)
        // if (Time.time >= nextFiretime)
        // {
        nextFiretime = Time.time + fireRate;

        GameObject bulletHolder;
        bulletHolder = Instantiate(bulletClip, transform.position, transform.rotation);

        Rigidbody _rigidbody;
        _rigidbody = bulletHolder.GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.right * bulletForce);

        Destroy(bulletHolder, 6.0f);
        // }
    }
}
