using UnityEngine;

[CreateAssetMenu(menuName = "Events/Mode Event Channel")]
public class ModeEventChannel : ScriptableObject 
{
    public delegate void ModeController(bool isInCombat);
    public ModeController SwitchModes;
    public void TriggerEvent(bool isInCombat) => SwitchModes?.Invoke(isInCombat);
}