using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bondfire : MonoBehaviour, IInteract
{
    [SerializeField] private VoidEventChannel advanceTime_EventChannel;

    public TextEvent Prompt => textPrompt;

    [SerializeField] private TextEvent textPrompt;
   
    private VoidEvent voidEvent;

    public void ExitInteract()
    {
        
    }

    public void Interact()
    {
        advanceTime_EventChannel.CallEvent(voidEvent);
    }

    public void OnStay()
    {
        
    }
}
