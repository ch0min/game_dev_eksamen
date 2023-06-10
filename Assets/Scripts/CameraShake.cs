using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform camera til at shake, og tage gameObject's transform hvis null.
    public Transform camTransform;

    // varighed på shake
    public float shakeDuration;

    // shaking styrke.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public Vector3 originalPos;

    private void Awake() {
        if (camTransform == null) camTransform = GetComponent(typeof(Transform)) as Transform;
    }

    private void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    public void Shake() {
        if (shakeDuration > 0f) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else {
            // Reset shakeDuration hvis man holder knappen nede.
            if (shakeDuration <= 0f) {
                shakeDuration = 1f;
                camTransform.localPosition = originalPos;
            }
        }
    }
}
