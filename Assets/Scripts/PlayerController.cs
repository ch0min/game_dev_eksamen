using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float health = 100f;
    [SerializeField]
    private float moveSpeed = 1;
    private Rigidbody rigidBody;
    private Camera mainCamera;
    private Vector2 moveVector;
    private CharacterController characterController;
    private Animator animator;
    
    public static Vector3 playerPos;



    public void ModifyHealth(float amount) {
        health += amount;

        if (health > 100) {
            health = 100;
        }
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        //TODO: add ragdoll instead of Destroy(gameObject)
        Destroy(gameObject);
    }

    void Start() {
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();

    }


    IEnumerator TrackPlayer() {
        while (true) {
            playerPos = gameObject.transform.position;
            yield return null;
        }
    }


    void Update() {
        Move();
    }

    private void FixedUpdate() {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength)) {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }


    public void OnMove(InputAction.CallbackContext context) {
        moveVector = context.ReadValue<Vector2>();

        if (Keyboard.current != null && Keyboard.current.wKey.wasPressedThisFrame) {
            // Debug.Log("D was pressed!");
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }
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

        if (Keyboard.current != null && Keyboard.current.dKey.wasPressedThisFrame) {
            // Debug.Log("D was pressed!");
            animator.SetBool("isRightStrafe", true);
        }
        else {
            animator.SetBool("isRightStrafe", false);
        }
        if (Keyboard.current != null && Keyboard.current.aKey.wasPressedThisFrame) {

            animator.SetBool("isLeftStrafe", true);
        }
        else {
            animator.SetBool("isLeftStrafe", false);
        }
        if (Keyboard.current != null && Keyboard.current.sKey.wasPressedThisFrame) {

            animator.SetBool("isBackwards", true);
        }
        else {
            animator.SetBool("isBackwards", false);
        }
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) {

            animator.SetBool("isRolling", true);
        }
        else {
            animator.SetBool("isRolling", false);
        }

    }

    private void Move() {
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
}
