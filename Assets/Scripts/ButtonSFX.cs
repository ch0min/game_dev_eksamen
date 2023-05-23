using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public AudioSource btnSFX;
    

    public void ClickSound() {
        btnSFX.Play();
    }
}
