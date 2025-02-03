using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TextController : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] AudioPitcherSO typingAudio;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI interactPrompt;
    [SerializeField] private TextMeshProUGUI headerPrompt;

    [Header("Typing Effect Settings")]
    [SerializeField] private float timePerLetter = 0.1f;

    private Queue<TextEvent> textQueue = new();

    private Coroutine textInstance = null;

    private AudioSource source;

    private bool isInteracting;

    public void InitializeAudioSource(GameObjectEvent ctx)
    {
        source = ctx.Object.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Displays interact text (Has higher priority than text event triggers)
    /// </summary>
    /// <param name="ctx"></param>
    public void InteractPrompt(TextEvent ctx)
    {
        StopAllCoroutines();

        interactPrompt.text = string.Empty;

        isInteracting = true;

        StartCoroutine(DisplayText(ctx.TextPrompt, ctx.ClearTime));
    }

    public void QueueTextPrompt(TextEvent ctx)
    {
        //Empty prompt bootstrap case
        if (ctx.TextPrompt == string.Empty)
        {
            StopAllCoroutines();
            interactPrompt.text = string.Empty;
            return;
        }

        //Queue text event
        textQueue.Enqueue(ctx);
    }

    public void QueueHeaderPrompt(TextEvent ctx)
    {
        headerPrompt.text = ctx.TextPrompt;
    }

    public void CallResetText(VoidEvent ctx) => ResetText();

    public void Update()
    {
        //Prevent queue from being used while interacting
        if (isInteracting) return;

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
            typingAudio.Play(source);

            interactPrompt.text += prompt[i];

            yield return waitTime;
        }

        //If interact prompt, don't clear
        if (clearTime != 0)
        {
            //Wait to clear
            yield return new WaitForSeconds(clearTime);

            ResetText();
        }
    }

    private void ResetText()
    {
        //Cancel current event
        StopAllCoroutines();

        //Clear event
        textInstance = null;

        isInteracting = false;

        interactPrompt.text = string.Empty;
    }
}
