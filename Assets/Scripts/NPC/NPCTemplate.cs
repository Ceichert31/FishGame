using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCTemplate : MonoBehaviour, IInteract
{
    //Event Channels
    [SerializeField] private TextEventChannel text_EventChannel;
    [SerializeField] private VoidEventChannel clearText_EventChannel;


    NPCBehaviorBase npcBehavior;
    TextEvent exitText => npcBehavior.ExitText;
    public TextEvent Prompt => npcBehavior.Hover_PromptText;

    void Awake()
    {
        try
        {
            npcBehavior = GetComponent<NPCBehaviorBase>();

        }
        catch
        {
            throw new System.Exception("This NPC does not have any special interaction behavior");
        }
    }


    void Update()
    {

    }

    public void ExitInteract()
    {
        npcBehavior.ExitInteractBehavior();
        text_EventChannel.CallEvent(exitText);
    }

    public void Interact()
    {
        npcBehavior.InteractBehavior();
        clearText_EventChannel.CallEvent(new());
        text_EventChannel.CallEvent(npcBehavior.CurrentLine);
    }

    public void OnStay()
    {

    }
}
