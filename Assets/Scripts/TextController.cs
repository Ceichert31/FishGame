using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI promptText;

    [Header("Typing Effect Settings")]
    [SerializeField] private float timePerLetter = 0.1f;

    private Queue<TextEvent> textQueue = new();

    private Coroutine textInstance = null;

    public void UpdateTextPrompt(TextEvent ctx)
    {
        //Empty prompt bootstrap case
        if (ctx.TextPrompt == string.Empty)
        {
            StopAllCoroutines();
            promptText.text = string.Empty;
            return;
        }

        //Queue text event
        textQueue.Enqueue(ctx);
    }

    public void Update()
    {
        if (textQueue.Count <= 0) return;

        if (textInstance == null)
        {
            //Remove oldest event
            TextEvent textEvent = textQueue.Dequeue();

            //Start new text event
            textInstance = StartCoroutine(DisplayText(textEvent.TextPrompt, textEvent.ClearTime));
        }
    }

    /// <summary>
    /// Writes text event
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="clearTime"></param>
    /// <returns></returns>
    IEnumerator DisplayText(string prompt, float clearTime)
    {
        //Delay between new character
        WaitForSeconds waitTime = new(timePerLetter);

        //Iterate through string
        for (int i = 0; i < prompt.Length; i++)
        {
            promptText.text += prompt[i];
            yield return waitTime;
        }

        //Wait to clear
        yield return new WaitForSeconds(clearTime);

        ResetText();
    }

    private void ResetText()
    {
        //Clear event
        textInstance = null;

        promptText.text = string.Empty;
    }
}
