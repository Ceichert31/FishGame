using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeEventChannel : ScriptableObject 
{
    public delegate void ModeController();
    public ModeController SwitchModes;
    public void TriggerEvent() => SwitchModes?.Invoke();
}