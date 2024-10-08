using UnityEngine;

[CreateAssetMenu(menuName = "Events/Text Event Channel")]
public class TextEventChannel : GenericEventChannel<TextEvent> {}

[System.Serializable]
public struct TextEvent
{
    public string TextPrompt;

    public float ClearTime;

    public TextEvent(string textPrompt, float clearTime)
    {
        TextPrompt = textPrompt;
        ClearTime = clearTime;
    }
}