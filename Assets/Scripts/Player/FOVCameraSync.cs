using UnityEngine;

public class FOVCameraSync : MonoBehaviour
{
    private Camera cam;

    private void Awake() => cam = GetComponent<Camera>();

    private void Update() => cam.fieldOfView = Camera.main.fieldOfView;
}
