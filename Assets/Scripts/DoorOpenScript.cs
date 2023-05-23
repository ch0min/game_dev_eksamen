using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenScript : MonoBehaviour
{
    [SerializeField]
    private GameObject doorRight;
    [SerializeField]
    private GameObject doorLeft;
    private bool isOpen;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !isOpen){
        doorRight.transform.Translate(Vector3.right);
        doorLeft.transform.Translate(Vector3.left);
        isOpen = true;
        }
    }
}
