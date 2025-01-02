using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> toggleableUIElements;

    /// <summary>
    /// Enables/Disables UI
    /// </summary>
    /// <param name="isEnabled"></param>
    public void SetUI(BoolEvent isEnabled)
    {
        //Enable/Disable all UI elements
        foreach(GameObject uiElement in toggleableUIElements) 
        {
            uiElement.SetActive(isEnabled.Value);
        }
    }
}
