using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenScript : MonoBehaviour
{
    [SerializeField]
    private GameObject doorRight;
    [SerializeField]
    private GameObject doorLeft;
    [SerializeField]
    private float doorOpenAmount = 5;

    private bool isOpen;

    public AudioSource doorOpenSFX;

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Player") && !isOpen) {
            doorRight.transform.Translate(Vector3.right * doorOpenAmount);
            doorLeft.transform.Translate(Vector3.left * doorOpenAmount);
            isOpen = true;
            doorOpenSFX.Play();
        }
    }
}
