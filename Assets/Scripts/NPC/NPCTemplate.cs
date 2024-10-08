using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTemplate : MonoBehaviour, IInteract
{
    //Event Channels
    [SerializeField] private TextEventChannel text_EventChannel;
    [SerializeField] private VoidEventChannel clearText_EventChannel;

    TextEvent promptText;
    [SerializeField] string npcName;
    [SerializeField] List<TextEvent> dialog = new List<TextEvent>();
    [SerializeField] TextEvent exitText;
    TextEvent noTalk;
    bool knowsName;


    
    int dialogIndex = 0;
    public TextEvent Prompt => promptText;

    void Awake()
    {
        promptText.TextPrompt = $"Press [E] to interact with ?????";
        TextEvent empty = new TextEvent("", 0, TextStyle.Interact);
        dialog.Add(empty);
        noTalk = new TextEvent("Rude", 2, TextStyle.Interact);
    }


    void Update()
    {

    }

    public void ExitInteract()
    {
        text_EventChannel.CallEvent(dialogIndex == 0 ? noTalk : exitText);
        dialogIndex = 0;

    }

    public void Interact()
    {
        Debug.Log("interacted");
        if(dialogIndex == dialog.Count)
        {
            dialogIndex = 0;
        }
        TextBehavior(dialogIndex);
        clearText_EventChannel.CallEvent(new());
        text_EventChannel.CallEvent(dialog[dialogIndex]);
        dialogIndex++;
    }

    public void OnStay()
    {

    }

    void TextBehavior(int dialogeCount)
    {
        switch (dialogeCount)
        {
            case 0:
                promptText.TextPrompt = $"Press [E] to interact with Mr. LureBottom WIgglesWorth The Third";
                break;
            case 1:
                promptText.TextPrompt = $"Press [E] to interact with Lure";
                break;
        }
    }
}
