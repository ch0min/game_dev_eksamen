using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

[CustomEditor(typeof(AIBehaviour))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI() {
        AIBehaviour fov = (AIBehaviour)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radiusFOV);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angleFOV / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angleFOV / 2);
        
        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radiusFOV);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radiusFOV);

        if (fov.canSeePlayer) {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
        
        if (fov.heardPlayerAI) {
            Handles.color = Color.blue;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }


    private Vector3 DirectionFromAngle(float eulerY, float angleInDegress) {
        angleInDegress += eulerY;

        return new Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }
    
 
}
