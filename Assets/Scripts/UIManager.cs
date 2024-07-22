using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] private RawImage transitionScreen;

    [Header("Typing Effect Settings")]
    [SerializeField] private float timePerLetter = 0.1f;

    [Header("Transition Settings")]
    [SerializeField] private float transitionSpeedMultiplier = 3f;

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

    public void StartTransition(VoidEvent ctx)
    {
        FadeOut();
    }

    IEnumerator FadeOut()
    {
        Color transitionColor = new Color(255, 255, 255, 0);

        while (transitionColor.a < 255)
        {
            transitionColor.a += Time.deltaTime * transitionSpeedMultiplier;

            transitionScreen.color = transitionColor;

            yield return null;
        }

        transitionScreen.color = new Color(255, 255, 255, 255);

        FadeIn();
    }

    IEnumerator FadeIn()
    {
        Color transitionColor = new Color(255, 255, 255, 255);

        while (transitionColor.a > 0)
        {
            transitionColor.a -= Time.deltaTime * transitionSpeedMultiplier;

            transitionScreen.color = transitionColor;

            yield return null;
        }

        transitionScreen.color = new Color(255, 255, 255, 0);
    }
}
