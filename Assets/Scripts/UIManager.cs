using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] private RawImage transitionScreen;

    [Header("Typing Effect Settings")]
    [SerializeField] private float timePerLetter = 0.1f;

    [Header("Transition Settings")]
    [SerializeField] private float transitionSpeedMultiplier = 3f;

    [Header("Grapple Bar Referenes")]
    [SerializeField] private Image pointer;

    [Header("Grapple Bar Settings")]
    [SerializeField] private float scrollSpeed = 3f;

    [SerializeField] private Transform endPos;
    private Vector3 currentPos => pointer.transform.position;

    private bool hasClicked;

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

    /// <summary>
    /// Starts attack minigame
    /// </summary>
    /// <param name="ctx"></param>
    public void StartMinigame(VoidEvent ctx)
    {
        pointer.transform.localPosition = Vector3.zero;
     
        StartCoroutine(AttackMinigame());
    }

    IEnumerator AttackMinigame()
    {
        RangedFloat rangedFloat;
        rangedFloat.minValue = -150f;
        rangedFloat.maxValue = -100f;

        int finalDamage = 0;

        while (Vector2.Distance(currentPos, endPos.position) > 0.1f)
        {
            pointer.transform.position = Vector2.MoveTowards(currentPos, endPos.position, scrollSpeed * Time.unscaledDeltaTime);

            //Click signal
            if (hasClicked)
            {
                hasClicked = false;

                //Check if click is valid
                if (currentPos.x >= rangedFloat.minValue && currentPos.x <= rangedFloat.maxValue)
                {
                    finalDamage++;
                }
                else
                {
                    Debug.Log("MISSED!!!");
                }
            }

            yield return null;
        }

        pointer.transform.position = endPos.position;

        Debug.Log(finalDamage);
    }

    public void PlayerClick(InputAction.CallbackContext ctx)
    {
        hasClicked = true;
    }

    public void InitializeControls(InputEvent ctx)
    {
        ctx.Action.ReelIn.Click.performed += PlayerClick;
    }
}
