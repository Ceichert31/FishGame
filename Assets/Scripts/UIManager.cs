using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI promptText;


    [Header("Typing Effect Settings")]
    [SerializeField] private float timePerLetter = 0.1f;

    public void UpdateTextPrompt(TextEvent ctx)
    {
        if (ctx.TextPrompt == string.Empty)
        {
            StopAllCoroutines();
            promptText.text = string.Empty;
            return;
        }
        StartCoroutine(DisplayText(ctx.TextPrompt));

        if (ctx.CanClear)
            Invoke(nameof(ClearText), ctx.ClearTime);
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

    void ClearText()
    {
        promptText.text = string.Empty;
    }
}