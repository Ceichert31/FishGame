using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Double Float event channel")]
public class DoubleFloatEventChannel : GenericEventChannel<DoubleFloatEvent> {}

[System.Serializable]
public struct DoubleFloatEvent
{
    public float ValueOne;

    public float ValueTwo;

    public DoubleFloatEvent(float valueOne, float valueTwo)
    {
        this.ValueOne = valueOne;

        this.ValueTwo = valueTwo;
    }
}