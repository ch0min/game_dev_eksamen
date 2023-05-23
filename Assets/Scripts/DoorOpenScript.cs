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
    [SerializeField] private string text = "The battle you're about to face will test your might and wit. Prepare yourself, for the epic confrontation awaits!";
    //TODO: maybe remove customSkin if it is unused 
    [SerializeField] private GUISkin customSkin;
    private bool guiOn;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !isOpen)
        {
        guiOn = true;
        doorRight.transform.Translate(Vector3.right);
        doorLeft.transform.Translate(Vector3.left);
        isOpen = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            guiOn = false;
        }
    }

    private void OnGUI()
    {
        if (guiOn)
        {
            if (customSkin != null)
            {
                GUI.skin = customSkin;
            }

            Rect boxSize = new Rect((Screen.width - 200) / 2, (Screen.height - 100) / 2, 200, 100);
            GUI.Label(boxSize, text);
        }
    }
}
