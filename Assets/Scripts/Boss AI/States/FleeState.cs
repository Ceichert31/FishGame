using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FleeState", menuName = "BossStates/Flee")]
public class FleeState : AIState
{
    Vector3 fleeLocation;
    Animator bossAnimator;

    bool called = false;
    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }


    public override void InitalizeState(BossAI ctx)
    {
        bossAnimator = bossTransform.GetComponent<Animator>();
    }

    public override void EnterState(BossAI ctx)
    {

    }

    public override void ExecuteState(BossAI ctx)
    {
        
    }

    public override void ExitState(BossAI ctx)
    {
        
    }
}
