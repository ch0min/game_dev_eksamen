using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHit : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 0.2f);
    }
}
