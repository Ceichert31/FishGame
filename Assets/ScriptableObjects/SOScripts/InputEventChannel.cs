using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Events/Input Event Channel")]
public class InputEventChannel : GenericEventChannel<InputEvent> { }
[System.Serializable]
public struct InputEvent
{
    public PlayerControls Action;
    public InputEvent(PlayerControls instance) => Action = instance; 
}
