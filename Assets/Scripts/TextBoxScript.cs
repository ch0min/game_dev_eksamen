using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxScript : MonoBehaviour
{
    [SerializeField]
    private string text;
    private bool guiOn;

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Player")) {
            guiOn = true;
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.CompareTag("Player")) {
            guiOn = false;
        }
    }

    private void OnGUI() {
        if (guiOn) {
            Rect boxSize = new Rect((Screen.width - 200) / 2, (Screen.height - 100) / 2, 200, 100);
            GUI.Label(boxSize, text);
        }
    }
}
