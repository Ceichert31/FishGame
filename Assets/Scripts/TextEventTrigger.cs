using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private TextEventChannel text_EventChannel;

    [Header("Text Event Settings")]
    [SerializeField] private string textPrompt;

    [Tooltip("How long it will take the text to clear")]
    [SerializeField] private float textClearTime = 2f;

    [Tooltip("Whether or not the text will clear")]
    [SerializeField] private bool canClearText = true;

    [Tooltip("Whether or not the trigger will be disabled after being interacted with")]
    [SerializeField] private bool isOneTimeUse = true;

    [Tooltip("Whether or not a second text prompt will be displayed")]
    [SerializeField] private bool hasSecondPrompt;

    [Tooltip("What willl be displayed in the second prompt")]
    [SerializeField] private string secondTextPrompt;

    [Tooltip("How long it will take for the second prompt to display")]
    [SerializeField] private float secondPromptDelayTime = 3f;

    private TextEvent textEvent;

    private const int PLAYERLAYER = 6;

    void Start()
    {
        //Initialize text event
        textEvent = new TextEvent(textPrompt, textClearTime, canClearText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PLAYERLAYER)
        {
            text_EventChannel.CallEvent(textEvent);

            //Disable after interaction
            if (isOneTimeUse)
                gameObject.SetActive(false);

            //Displays a second prompt after first one clears
            if (hasSecondPrompt)
                Invoke(nameof(DisplaySecondTextPrompt), secondPromptDelayTime);
        }
            
    }

    /// <summary>
    /// Displays a second prompt after the first clearss
    /// </summary>
    private void DisplaySecondTextPrompt()
    {
        //Set new text
        textEvent.TextPrompt = secondTextPrompt;

        text_EventChannel.CallEvent(textEvent);
    }
}
