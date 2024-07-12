using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIEventChannel ui_EventChannel;


    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI promptText;


    [Header("Typing Effect Settings")]
    [SerializeField] private float timePerLetter = 0.1f;


    public void UpdateTextPrompt(string prompt)
    {
        if (prompt == string.Empty)
        {
            StopAllCoroutines();
            promptText.text = string.Empty;
            return;
        }
        StartCoroutine(DisplayText(prompt));
    }

    IEnumerator DisplayText(string prompt)
    {
        WaitForSeconds waitTime = new(timePerLetter);

        for (int i = 0; i < prompt.Length; i++)
        {
            promptText.text += prompt[i];
            yield return waitTime;
        }
    }

    private void OnEnable()
    {
        ui_EventChannel.UpdatePrompt += UpdateTextPrompt;
    }
    private void OnDisable()
    {
        ui_EventChannel.UpdatePrompt -= UpdateTextPrompt;
    }
}
