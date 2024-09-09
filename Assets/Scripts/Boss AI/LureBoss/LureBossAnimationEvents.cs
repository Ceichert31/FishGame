using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

public class LureBossAnimationEvents : AnimationEvents
{

    public override void TeleportBoss()
    {
        Debug.Log("called");
        FleeState fleeState = (FleeState)bossAi.BossStates[(int)States.FleeState];
        fleeState.TeleportFish();
    }
}
