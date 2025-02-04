using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCamera : MonoBehaviour
{
    [SerializeField] private ComponentEventChannel componentEventChannel;

    [SerializeField] private BoolEventChannel setUIEventChannel;

    [SerializeField] private CameraEventChannel setActiveCamera;

    [SerializeField] private Camera cinematicCamera;
    /// <summary>
    /// Sets main camera
    /// </summary>
    /// <param name="isEnabled"></param>
    public void SetCamera(bool isEnabled)
    {
        //Enable/disable player camera and audio listener

        //Update current main camera
       /* if (isEnabled)
            setActiveCamera.CallEvent(new(playerCamera));
        else
            setActiveCamera.CallEvent(new(cinematicCamera));*/

        componentEventChannel.CallEvent(new(isEnabled, isEnabled, isEnabled, isEnabled));

        setUIEventChannel.CallEvent(new(isEnabled));
    }
}
