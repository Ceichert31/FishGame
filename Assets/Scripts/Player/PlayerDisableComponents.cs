using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisableComponents : MonoBehaviour
{
    private AudioListener audioComponent;
    private Camera cameraComponenet;
    private InputController inputComponent;

    private void Start()
    {
        audioComponent = transform.GetChild(0).GetChild(0).GetComponent<AudioListener>();
        cameraComponenet = transform.GetChild(0).GetChild(0).GetComponent<Camera>();

        inputComponent = GetComponent<InputController>();
    }

    /// <summary>
    /// Allows player components to be enabled/disabled
    /// </summary>
    public void SetComponents(ComponentEvent ctx)
    {
        audioComponent.enabled = ctx.IsAudioActive;
        cameraComponenet.enabled = ctx.IsCameraActive;
        inputComponent.SetInputActions(ctx.IsInputActive, ctx.IsLookActive);
    }
}
