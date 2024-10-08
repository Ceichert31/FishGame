using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for implimenting NPC Behavior
/// </summary>
public abstract class NPCBehaviorBase : MonoBehaviour
{
    //Name of the npc
    [SerializeField] protected string npcName;

    //Text prompt that is shown when hovering over an npc
    protected TextEvent hover_PromptText;

    //Dialog that the player will see, it is added to and edited in the inspector
    [SerializeField] protected List<TextEvent> dialog = new List<TextEvent>();

    //This is a text event used a place holder between the yes and no talk exit texts
    protected TextEvent exitTextEvent;

    //exit text event triggered after leaving the npc when the player has sufficiently talked to the npc
    [SerializeField] protected TextEvent yesTalk_exitText;

    //exit text event triggered after leaving the npc when the player has not talked to the npc enough
    [SerializeField] protected TextEvent noTalk_ExitText;

    //Index of the dialog we are currently one(starts at -1 as that allows the dialog to be indexed from 0)
    protected int dialogIndex = -1;

    //Index for the introduction of an npc, if an npc does not have an introduction just leave it at 0
    [Header("Index For Skipping Introductions")]
    [SerializeField] protected int introductionIndex;

    /// <summary>
    /// Returns the current line of dialog the npc is on
    /// </summary>
    public TextEvent CurrentLine
    {
        get { return dialog[dialogIndex]; }
    }

    /// <summary>
    /// Returns the exit text that is played when exiting the npc's interaction radius
    /// </summary>
    public TextEvent ExitText
    {
        get { return exitTextEvent; }
    }
    
    /// <summary>
    /// Returns the hover prompt for the npc
    /// </summary>
    public TextEvent Hover_PromptText
    {
        get { return hover_PromptText; }
    }

    //Sets the exit text to the no talk exit text as that makes the most sense, and sets the hover prompt to the interaction key + question marks
    private void Awake()
    {
        exitTextEvent = noTalk_ExitText;
        hover_PromptText.TextPrompt = $"Press [E] to interact with ?????";
    }

    //Adds a dialog to the end of dialogs as to let players know that an npc is out of dialog
    private void Start()
    {
        dialog.Add(new("", 0));
    }

    /// <summary>
    /// Abstract method that defines any special interaction behavior an NPC requires
    /// </summary>
    public abstract void InteractBehavior();

    /// <summary>
    /// Virtual method that defines the on exit interaction with npcs
    /// </summary>
    public virtual void ExitInteractBehavior()
    {
        if (dialogIndex >= introductionIndex)
        {
            dialogIndex = introductionIndex;
            exitTextEvent = yesTalk_exitText;
            UpdateName();
        }
        else
        {
            exitTextEvent = noTalk_ExitText;
            dialogIndex = -1;
        }
    }

    /// <summary>
    /// Method that updates the name of the npc
    /// </summary>
    public void UpdateName()
    {
        hover_PromptText.TextPrompt = $"Press [E] to Interact with {name}";
    }
}



/*Things that I want to discuss:
 * Should I keep the name system in this class or have each child regulate their own name and how it is displayed
 * Maybe the Interact Behavior can be made virtual, would not be a massive change but is something to think about
 */