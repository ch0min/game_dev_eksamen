using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]


    public float Health = 100f;

    private float moveSpeed = 1;

    private Rigidbody rigidBody;

    private Camera mainCamera;

    private Vector2 moveVector;

    private CharacterController characterController;

    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        Move();
    }
    
    private void FixedUpdate()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
    

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();

        if (Keyboard.current != null && Keyboard.current.wKey.wasPressedThisFrame)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        
        if (Keyboard.current != null && Keyboard.current.sKey.wasPressedThisFrame)
        {

            animator.SetBool("isBackwards", true);
        }
        else
        {
            animator.SetBool("isBackwards", false);
        }

    }

    private void Move()
    {
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
}
