using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channel/Field of View", fileName = "FOVEventChannel")]
public class FOVEventChannel : ScriptableObject
{
    public delegate void FOVController();
    public event FOVController FOVControllerUpdate;
    public void IncreaseFOV() => FOVControllerUpdate?.Invoke();
}
