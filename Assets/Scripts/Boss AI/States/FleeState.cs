using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FleeState", menuName = "BossStates/Flee")]
public class FleeState : AIState
{
    bool called = false;
    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }


    public override void InitalizeState(BossAI ctx)
    {
        
    }

    public override void EnterState(BossAI ctx)
    {
        //Find random position far away
        //Play going underwater animation
    }

    public override void ExecuteState(BossAI ctx)
    {
        
    }

    public override void ExitState(BossAI ctx)
    {
        
    }
}
