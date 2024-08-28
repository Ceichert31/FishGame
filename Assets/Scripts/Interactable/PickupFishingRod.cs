using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFishingRod : MonoBehaviour, IInteract
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private TextEventChannel text_EventChannel;
    [SerializeField] private VoidEventChannel clearText_EventChannel;
    [SerializeField] private VoidEventChannel enablePole_EventChannel;

    public TextEvent Prompt { get => textEvent; }

    [SerializeField] private TextEvent textEvent;

    [SerializeField] private List<TextEvent> tutorialText;

    public void ExitInteract()
    {
        
    }

    public void Interact()
    {
        //Disable model
        gameObject.SetActive(false);

        clearText_EventChannel.CallEvent(new());

        enablePole_EventChannel.CallEvent(new());

        //Enable fishing rod

        //Enable tutorial
        foreach(TextEvent textPrompt in tutorialText)
            text_EventChannel.CallEvent(textPrompt);
    }

    public void OnStay()
    {
        
    }
}
