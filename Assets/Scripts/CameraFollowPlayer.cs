using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform followPlayer;
    private Transform cameraTransform;

    // Position
    public Vector3 playerOffset;

    // Lerp Speed
    public float lerpSpeed = 5f;

    void Start() {
        cameraTransform = transform;
    }

    void SetTarget(Transform newTransformTarget) {
        // Opdaterer player transform
        followPlayer = newTransformTarget;
    }

    void LateUpdate() {
        if (followPlayer != null) {
            Vector3 targetPosition = followPlayer.position + playerOffset;
            // Camera Lerp effect
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, lerpSpeed * Time.deltaTime);
        }
    }
}
