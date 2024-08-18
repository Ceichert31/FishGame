using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LureStaggerState", menuName = "BossStates/Stagger")]
public class StaggerState : AIState
{
    Animator bossAnimator;
    private bool called = false;

    private float staggerTime;
    bool canExit;
    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }

    public override void EnterState(BossAI ctx)
    {
        bossAnimator.SetTrigger("Staggered");
    }

    public override void ExecuteState(BossAI ctx)
    {
        float timer = Time.time + staggerTime;

        if (timer < Time.time || canExit)
        {
            ctx.SwitchState(States.WalkState);
            return;
        }
    }

    public override void ExitState(BossAI ctx)
    {
        canExit = false;
        bossAnimator.SetTrigger("NoLongerStaggered");
        throw new System.NotImplementedException();
    }

    public override void InitalizeState(BossAI ctx)
    {
        bossAnimator = bossTransform.GetComponent<Animator>();
    }

    public void NoLongerStaggered(FloatEvent ctx)
    {
        canExit = true;
    }
}
