using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Events/Input Event Channel")]
public class InputEventChannel : GenericEventChannel<InputEvent> 
{
    public void SwitchControlModes(InputEvent ctx, bool isInCombat)
    {
        if (isInCombat)
        {
            ctx.Action.Combat.Enable();
        }
        else
        {
            ctx.Action.Combat.Disable();
        }
    }
}
[System.Serializable]
public struct InputEvent
{
    public PlayerControls Action;
    public InputEvent(PlayerControls instance) => Action = instance; 
}
