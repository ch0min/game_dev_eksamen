using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake() {
        if (camTransform == null) {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    void Update() {
        if (Input.GetButton("Fire1")) {
            if (shakeDuration > 0f) {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else {
                // Reset shakeDuration if the button is held down
                if (shakeDuration <= 0f) {
                    shakeDuration = 0f;
                    camTransform.localPosition = originalPos;
                }
            }
        }
        else {
            // Reset shakeDuration when the button is released
            shakeDuration = 100f;
            camTransform.localPosition = originalPos;
        }
    }

}
