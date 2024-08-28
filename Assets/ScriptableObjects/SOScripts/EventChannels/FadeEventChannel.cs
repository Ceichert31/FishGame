using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Fade Event Channel")]
public class FadeEventChannel : GenericEventChannel<FadeEvent> {}

[System.Serializable]
public struct FadeEvent
{
    public bool fadeIn;

    public Color fadeColor;

    public FadeEvent(bool boolValue, Color colorValue)
    {
        fadeIn = boolValue;
        fadeColor = colorValue;
    }
}