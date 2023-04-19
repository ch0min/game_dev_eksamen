using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController controller;
    Vector2 movement;
    public float moveSpeed;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;
        if(direction.magnitude >= 0.1f)
        {
            controller.Move(direction * moveSpeed * Time.deltaTime); 
        }
    }
}
