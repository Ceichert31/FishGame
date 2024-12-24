using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCamera : MonoBehaviour
{
    private GameObject cam;

    private void Awake()
    {
        cam = Camera.main.transform.gameObject;
    }

    /// <summary>
    /// Sets main camera
    /// </summary>
    /// <param name="isEnabled"></param>
    public void SetCamera(bool isEnabled)
    {
        cam.SetActive(isEnabled);
    }
}
