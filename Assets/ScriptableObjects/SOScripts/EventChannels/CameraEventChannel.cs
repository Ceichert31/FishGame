using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Camera Event Channel")]
public class CameraEventChannel : GenericEventChannel<CameraEvent> {}

[System.Serializable]
public struct CameraEvent
{
    public Camera Value;

    public CameraEvent(Camera value)
    {
        Value = value;
    }
}
