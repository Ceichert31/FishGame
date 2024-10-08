using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class LureTutorialNPCBehavior : NPCBehaviorBase
{
    [SerializeField] Animator animator;
    

    public override void InteractBehavior()
    {
        dialogIndex++;

        if (dialogIndex >= dialog.Count)
        {
            dialogIndex = introductionIndex;
        }

        switch(dialogIndex)
        {
            case 0:
                animator.SetTrigger("Introduction");
                npcName = "Mr.LureBottom WIgglesWorth The Third";
                UpdateName();
                break;
            case 1:
                npcName = "Lure";
                break;
        }
    }
}
