using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        throw new System.NotImplementedException();
    }

    public override void EnterState(BossAI ctx)
    {
        throw new System.NotImplementedException();
    }

    public override void ExecuteState(BossAI ctx)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(BossAI ctx)
    {
        throw new System.NotImplementedException();
    }
}
