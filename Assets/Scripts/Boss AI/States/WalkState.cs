using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;
public class WalkState : AIState
{
    float maxDistance;
    public override void EnterState(BossAI ctx)
    {
        ctx.Agent.speed = 5f;

        //Play idle animation
    }

    public override void ExecuteState(BossAI ctx)
    {


        /*
         * Walking state code:
         * Ok so this should be simmilar to idle in terms of a more of an in between attacks gap closer type behvior
         * Maybe have the boss causally fire projectile patters as he walks toward the player for physical attacks
         * 
         * Exit Condition:
         * We become in range of the player to do whatever out next attack is
         * We get successflly grappled and mounted
         */

        //Exit Condition(temps)
        if(Util.DistanceNoY(Player, transform.position) < maxDistance)
        {
            ctx.SwitchState(ctx.idleState);
        }
    }

    public override void ExitState(BossAI ctx)
    {
        
    }
}
