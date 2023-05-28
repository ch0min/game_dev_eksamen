using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;

    private void Awake()
    {
        if (camTransform == null) camTransform = GetComponent(typeof(Transform)) as Transform;
    }

    private void Update()
    {
        // Shake();
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    public void Shake()
    {
        // if (Input.GetButton("Fire1"))
        // {
        if (shakeDuration > 0f)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            // Reset shakeDuration if the button is held down
            if (shakeDuration <= 0f)
            {
                shakeDuration = 1f;
                camTransform.localPosition = originalPos;
            }
        }
        // }
        // else
        // {
        //     // Reset shakeDuration when the button is released
        //     shakeDuration = 1f;
        //     camTransform.localPosition = originalPos;
        // }
    }
}