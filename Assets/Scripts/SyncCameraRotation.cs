using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCameraRotation : MonoBehaviour
{
    private Transform cam => Camera.main.transform;

    void LateUpdate()
    {
        transform.eulerAngles = cam.transform.eulerAngles;
    }
}
