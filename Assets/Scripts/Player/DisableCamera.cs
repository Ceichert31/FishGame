using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCamera : MonoBehaviour
{
    [SerializeField] private BoolEventChannel setUIEventChannel;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    /// <summary>
    /// Sets main camera
    /// </summary>
    /// <param name="isEnabled"></param>
    public void SetCamera(bool isEnabled)
    {
        cam.enabled = isEnabled;
        setUIEventChannel.CallEvent(new(isEnabled));
    }
}
