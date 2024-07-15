using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingEventChannel : GenericEventChannel<FishingEvent>
{
    
}
[System.Serializable]
public struct FishingEvent
{
    public delegate void FishingControllerDelegate(bool isCast);
    public event FishingControllerDelegate CastPole;

    public InputAction reelIn;

    public FishingEvent(InputAction input, FishingControllerDelegate instance)
    {
        reelIn = input;
        CastPole = instance;
    }
}
