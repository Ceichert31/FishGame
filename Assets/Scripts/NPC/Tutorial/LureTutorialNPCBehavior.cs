using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class LureTutorialNPCBehavior : NPCBehaviorBase
{

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
                name = "Mr.LureBottom WIgglesWorth The Third";
                UpdateName();
                break;
        }
    }
}
