using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    private float idleTime = 3f;

    public override void EnterState(BossAI ctx)
    {
        ctx.Agent.speed = 0;

        //Play idle animation
    }

    public override void ExecuteState(BossAI ctx)
    {
        float currentTime = Time.time + idleTime;

        if (currentTime < Time.time)
            ctx.SwitchState(ctx.walkState);

        /*Idle Code:
         * probably going to switch into this for at least a little bit after each major attack so good job on the timer
         * I would like some code that checks if we are trying to be grappled to during this state and do something with that info(if we are not ready to be grappled yet shake the mf off
         * and go straight to attacking
         * Probably will make a "can grapple to" kind of boolean on the base class for brevaties sake
         * 
         * Exit Condition:
         * We are ready to do our next attack
        */
    }

    public override void ExitState(BossAI ctx)
    {
        
    }
}
