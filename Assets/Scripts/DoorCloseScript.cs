using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject doorRight;
    [SerializeField]
    private GameObject doorLeft;

    private bool isOpen = true;

    void OnTriggerEnter(Collider col) {
        if (isOpen) {
            doorRight.transform.Translate(Vector3.left);
            doorLeft.transform.Translate(Vector3.right);
            isOpen = false;
        }
    }
}
