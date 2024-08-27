using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Key Event Channel")]
public class KeyEventChannel : GenericEventChannel<KeyEvent> {}

[System.Serializable]
public struct KeyEvent
{
    public int keyID;

    public bool isCollected;

    public KeyEvent(int id, bool collected)
    {
        keyID = id;

        isCollected = collected;
    }
}