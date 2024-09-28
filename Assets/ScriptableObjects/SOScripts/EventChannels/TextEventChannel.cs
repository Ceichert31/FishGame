using UnityEngine;

[CreateAssetMenu(menuName = "Events/Text Event Channel")]
public class TextEventChannel : GenericEventChannel<TextEvent> {}

[System.Serializable]
public struct TextEvent
{
    public string TextPrompt;

    public float ClearTime;

    public TextStyle TextStyle;

    public TextEvent(string textPrompt, float clearTime, TextStyle textStyle)
    {
        TextPrompt = textPrompt;
        ClearTime = clearTime;
        TextStyle = textStyle;
    }
}