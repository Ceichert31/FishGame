using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

public class LureBossAnimationEvents : AnimationEvents
{
    public override void EndAttacking()
    {
        AttackState attackState = (AttackState)bossAi.BossStates[(int)States.AttackState];
        attackState.Attacking = false;
        Debug.Log("attack ended");
    }

    public override void TeleportBoss()
    {
        Debug.Log("called");
        FleeState fleeState = (FleeState)bossAi.BossStates[(int)States.FleeState];
        fleeState.TeleportFish();
    }
}
