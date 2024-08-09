using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI promptText;

    [Header("Typing Effect Settings")]
    [SerializeField] private float timePerLetter = 0.1f;

    public void UpdateTextPrompt(TextEvent ctx)
    {
        //Cancel any previous textPrompts
        StopAllCoroutines();

        promptText.text = string.Empty;

        //Empty prompt bootstrap case
        if (ctx.TextPrompt == string.Empty)
        {
            StopAllCoroutines();
            promptText.text = string.Empty;
            return;
        }

        StartCoroutine(DisplayText(ctx.TextPrompt));
        
        //Clear text after waitTime
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
