using UnityEngine;

public class FOVCameraSync : MonoBehaviour
{
    private Camera mainCam;

    private Camera cam;

    private void Awake()
    {
        mainCam = Camera.main;

        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        if (mainCam.enabled == false) return;

        cam.fieldOfView = Camera.main.fieldOfView;
    }
}
