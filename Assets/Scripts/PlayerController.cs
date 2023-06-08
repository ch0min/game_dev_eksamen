using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float health = 100f;

    [SerializeField]
    private float moveSpeed = 1;

    private Animator animator;
    private CharacterController characterController;
    public static Vector3 playerPos;
    private float gravity = 9.81f;
    public GameObject deadBody;
    bool created = false;
    public GameObject DeathCanvas;


    private Camera mainCamera;
    private Vector2 moveVector;
    private Rigidbody rigidBody;

    public BulletProjectile bulletProjectile;
    public CameraShake cameraShake;
    public BulletClip bulletClip;

    private int currentAmmo;
    private int magazineSize = 30;
    private int reserveAmmo = 30;
    private float reloadDuration = 1.5f; // Reload in seconds
    private bool isReloading = false;
    public Text ammoText;

    public AudioSource reloadSFX;

    private void Start() {
        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();
        currentAmmo = magazineSize;
    }

    private void Update() {
        Move();
        if (Input.GetButton("Fire1")) {
            Fire();
        }
        else {
            // Reset shakeDuration when the button is released
            cameraShake.shakeDuration = 1f;
            cameraShake.camTransform.localPosition = cameraShake.originalPos;

        }

        if (Input.GetKeyDown((KeyCode.R))) {
            StartCoroutine(ReloadCoroutine());
        }
        ammoText.text = currentAmmo + "/" + reserveAmmo;
    }

    private void FixedUpdate() {
        var cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength)) {
            var pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    public void AddAmmo(int amount) {
        reserveAmmo += amount;
    }

    public void ModifyHealth(float amount) {
        health += amount;
        if (health > 100) health = 100;
        if (health <= 0) {
            if (!created) {
                Instantiate(deadBody, transform.position, transform.localRotation);
                created = true;
            }
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
        DeathCanvas.SetActive(true);
    }

    public void Fire() {
        if (currentAmmo > 0 && !isReloading) {
            cameraShake.Shake();

            if (Time.time >= bulletProjectile._nextFireTime) {
                bulletProjectile._nextFireTime = Time.time + bulletProjectile.fireRate;
                bulletProjectile.Shoot();
                bulletClip.ShootClip();
                currentAmmo--;
            }
        }
        else {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine() {
        if (!isReloading && currentAmmo < magazineSize && reserveAmmo > 0) {
            isReloading = true;
            yield return new WaitForSeconds(reloadDuration);
            int ammoNeeded = magazineSize - currentAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo);

            currentAmmo += ammoToReload;
            reserveAmmo -= ammoToReload;

            isReloading = false;
            reloadSFX.Play();
        }
    }


    public void OnMove(InputAction.CallbackContext context) {
        moveVector = context.ReadValue<Vector2>();

        if (Keyboard.current != null && Keyboard.current.wKey.wasPressedThisFrame)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
        if (Keyboard.current != null && Keyboard.current.dKey.wasPressedThisFrame)
            animator.SetBool("isRightStrafe", true);
        else
            animator.SetBool("isRightStrafe", false);
        if (Keyboard.current != null && Keyboard.current.aKey.wasPressedThisFrame)
            animator.SetBool("isLeftStrafe", true);
        else
            animator.SetBool("isLeftStrafe", false);
        if (Keyboard.current != null && Keyboard.current.sKey.wasPressedThisFrame)
            animator.SetBool("isBackwards", true);
        else
            animator.SetBool("isBackwards", false);
    }

    private void Move() {
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;

        if (!characterController.isGrounded) {
            move.y -= gravity * Time.deltaTime;
        }

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
}
