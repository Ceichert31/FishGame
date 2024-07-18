using UnityEngine;

[CreateAssetMenu(menuName = "Events/UI Event Channel")]
public class TextEventChannel : GenericEventChannel<TextEvent> {}

[System.Serializable]
public struct TextEvent
{
    public string TextPrompt;

    public float ClearTime;

    public bool CanClear;

    public TextEvent(string textPrompt, float clearTime, bool canClear)
    {
        TextPrompt = textPrompt;
        ClearTime = clearTime;
        CanClear = canClear;
    }
}