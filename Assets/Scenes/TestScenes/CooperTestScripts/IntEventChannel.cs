using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Events/IntEventChannel")]
public class IntEventChannel : GenericEventChannel<IntEvent>
{

}

[System.Serializable]
public struct IntEvent
{
    public int Value;

    public IntEvent(int instance) => Value = instance;
}
