using UnityEngine;

[CreateAssetMenu(menuName = "Events/UI Event Channel")]
public class UIEventChannel : ScriptableObject 
{
    public delegate void UpdateTextPrompt(string prompt);
    public UpdateTextPrompt UpdatePrompt;

    public void TriggerEvent(string prompt) => UpdatePrompt?.Invoke(prompt);
}

/*[System.Serializable]
public struct UIUpdateEvent
{
    public UIManager UIManager;
    public UIUpdateEvent(UIManager manager) => UIManager = manager;
}*/