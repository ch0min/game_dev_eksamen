using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float health = 100f;

    [SerializeField] private float moveSpeed = 1;

    private Animator animator;
    private CharacterController characterController;
    public static Vector3 playerPos;
    private float gravity = 9.81f;

    private Camera mainCamera;
    private Vector2 moveVector;
    private Rigidbody rigidBody;

    public BulletProjectile bulletProjectile;
    public CameraShake cameraShake;
    public BulletClip bulletClip;

    private int currentAmmo;
    private int magazineSize = 100;
    private int reserveAmmo = 100;
    private float reloadDuration = 1.5f; // Duration of the reload action in seconds
    private bool isReloading = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();
        currentAmmo = magazineSize;
    }

    private void Update()
    {
        Move();
        if (Input.GetButton("Fire1")) Fire();
        Debug.Log(currentAmmo + "/" + reserveAmmo);
    }

    private void FixedUpdate()
    {
        var cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            var pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
    }

    public void ModifyHealth(float amount)
    {
        health += amount;
        if (health > 100) health = 100;
        if (health <= 0) Die();
    }

    public void Die()
    {
        //TODO: add ragdoll instead of Destroy(gameObject)
        Destroy(gameObject);
    }

    public void Fire()
    {
        if (currentAmmo > 0)
        {
            bulletProjectile.Shoot();
            bulletClip.ShootClip();
            cameraShake.Shake();

            currentAmmo--;
            //animator.SetTrigger("Shooting");
        }
        else
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        while (!isReloading)
        {
            isReloading = true;
            // reloadProgress = 0f;

            // Play the reload animation
            // animator.SetBool("Reloading", true);

            // Wait for the reload duration
            yield return new WaitForSeconds(reloadDuration);

            // Refill the magazine and subtract the used ammo from the reserve
            int ammoNeeded = magazineSize - currentAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo);

            currentAmmo += ammoToReload;
            reserveAmmo -= ammoToReload;

            // Stop the reload animation and reset the reloading flag
            // animator.SetBool("Reloading", false);
            isReloading = false;

            // Update the reload progress to 0
            // reloadProgress = 0f;
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();

        if (Keyboard.current != null && Keyboard.current.wKey.wasPressedThisFrame)
            // Debug.Log("D was pressed!");
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
        // if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame) {
        //     // Debug.Log("D was pressed!");
        //     animator.SetBool("isRolling", true);
        // }
        // else {
        //     animator.SetBool("isRolling", false);
        // }
        /*if(moveVector.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            
        } */

        if (Keyboard.current != null && Keyboard.current.dKey.wasPressedThisFrame)
            // Debug.Log("D was pressed!");
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
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            animator.SetBool("isRolling", true);
        else
            animator.SetBool("isRolling", false);
    }

    private void Move()
    {
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;

        if (!characterController.isGrounded)
        {
            move.y -= gravity * Time.deltaTime;
        }

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
}