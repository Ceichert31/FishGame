using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LureStaggerState", menuName = "BossStates/Stagger")]
public class StaggerState : AIState
{
    [SerializeField] GameObject grapplePoint;

    [SerializeField] FloatEventChannel staggerTime_EventChannel;
    private bool called = false;
    bool staggered = false;
    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }

    public override void EnterState(BossAI ctx)
    {
        throw new System.NotImplementedException();
    }

    public override void ExecuteState(BossAI ctx)
    {
        //If no longer staggered
        if(!staggered)
        {
            ctx.SwitchState(ctx.walkState);
            return;
        }


    }

    public override void ExitState(BossAI ctx)
    {
        throw new System.NotImplementedException();
    }

    public override void InitalizeState(BossAI ctx)
    {
        //FindGrapplePoint
        //grapplePoint = 
    }

    /// <summary>
    /// Method to be called by float event channel
    /// This method just sets the boolean to tell the code whether the boss is still staggered or not
    /// </summary>
    /// <param name="ctx"></param>
    public void StaggeredState(FloatEvent ctx)
    {
        staggered = ctx.FloatValue > 0;
    }
}
