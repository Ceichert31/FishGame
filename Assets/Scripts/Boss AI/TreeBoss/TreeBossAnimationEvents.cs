using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBossAnimationEvents : AnimationEvents
{

    public override void EndAttacking()
    {
        TreeAttackState attackState = (TreeAttackState)bossAi.BossStates[(int)States.AttackState];
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
