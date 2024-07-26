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
    }

    public override void ExitState(BossAI ctx)
    {
        
    }
}
