using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Key Event Channel")]
public class KeyEventChannel : GenericEventChannel<KeyEvent> {}

[System.Serializable]
public struct KeyEvent
{
    public int ID;

    public bool Value;

    public KeyEvent(int id, bool value)
    {
        ID = id;

        Value = value;
    }
}