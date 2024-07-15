using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolEventChannel : GenericEventChannel<BoolEvent> { }
[System.Serializable]
public struct BoolEvent
{
    public bool Value;
    public BoolEvent(bool instance) => Value = instance;
}
