using UnityEngine;

[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannel : GenericEventChannel<StringEvent> { }

[System.Serializable]
public struct StringEvent
{
    public string text;
    public StringEvent(string instance) => text = instance;
}
