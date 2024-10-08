using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNPCBehavior : NPCBehaviorBase
{

    public override void ExitInteractBehavior()
    {
        base.ExitInteractBehavior();
    }

    public override void InteractBehavior()
    {
        dialogIndex++;
        if (dialogIndex >= dialog.Count)
        {
            dialogIndex = 0;
        }
    }
}
