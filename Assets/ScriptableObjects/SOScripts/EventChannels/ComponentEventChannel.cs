using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Component Event Channel")]
public class ComponentEventChannel : GenericEventChannel<ComponentEvent> {}

[System.Serializable]
public struct ComponentEvent
{
    public bool IsAudioActive;
    public bool IsCameraActive;
    public bool IsInputActive;
    public bool IsLookActive;

    public ComponentEvent(bool isAudioActive, bool isCameraActive, bool isInputActive, bool isLookActive)
    {
        IsAudioActive = isAudioActive;
        IsCameraActive = isCameraActive;
        IsInputActive = isInputActive;
        IsLookActive = isLookActive;
    }
}
