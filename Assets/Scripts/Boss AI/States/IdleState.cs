using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState",menuName ="BossStates/Idle")]
public class IdleState : AIState
{
    [SerializeField] private float idleTime;

    [SerializeField] private bool called = false;

    private float currentTime;

    protected override bool Called
    {
        get {return called; }
        set { called = value; }
    }
    public override void InitalizeState(BossAI ctx)
    {
        ctx.Agent.speed = 0;
        idleTime = 3f;
    }

    public override void EnterState(BossAI ctx)
    {
        currentTime = Time.time + idleTime;
        //Play idle animation
    }

    public override void ExecuteState(BossAI ctx)
    {
        //Wait for idle time
        if (currentTime < Time.time)
        {
            ctx.SwitchState(States.WalkState);
        }

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
