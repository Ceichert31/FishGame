using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : AIState
{
    public override void EnterState(BossAI ctx)
    {
        ctx.Agent.speed = 5f;

        //Play idle animation
    }

    public override void ExecuteState(BossAI ctx)
    {

    }

    public override void ExitState(BossAI ctx)
    {
        
    }
}
