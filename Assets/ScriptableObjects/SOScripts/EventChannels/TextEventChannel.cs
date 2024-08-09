using UnityEngine;

[CreateAssetMenu(menuName = "Events/UI Event Channel")]
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