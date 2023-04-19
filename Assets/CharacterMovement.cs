using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody rigidBody;
    public float speed = 4;
    Vector3 lookPos;
    Animator anim;
   
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
       
        SetupAnimator();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 100))
        {
            lookPos = hit.point;
        }
        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        rigidBody.AddForce(movement * speed / Time.deltaTime);
        anim.SetFloat("Forward", vertical);
        anim.SetFloat("Turn", horizontal);
    }

    void SetupAnimator()
    {
        anim = GetComponent<Animator>();
        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if(childAnimator != anim)   
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }
}
