using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCamera : MonoBehaviour
{
    [SerializeField] private BoolEventChannel setUIEventChannel;

    [SerializeField] private CameraEventChannel setActiveCamera;

    [SerializeField] private Camera cinematicCamera;

    private Camera playerCamera;

    private AudioListener audioListener;

    private void Awake()
    {
        playerCamera = Camera.main;
        audioListener = playerCamera.GetComponent<AudioListener>();
    }

    /// <summary>
    /// Sets main camera
    /// </summary>
    /// <param name="isEnabled"></param>
    public void SetCamera(bool isEnabled)
    {
        //Enable/disable player camera and audio listener
        playerCamera.enabled = isEnabled;
        audioListener.enabled = isEnabled;

        //Update current main camera
        if (isEnabled)
            setActiveCamera.CallEvent(new(playerCamera));
        else
            setActiveCamera.CallEvent(new(cinematicCamera));

        setUIEventChannel.CallEvent(new(isEnabled));
    }
}
