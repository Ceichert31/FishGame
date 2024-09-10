using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LureStaggerState", menuName = "BossStates/Stagger")]
public class StaggerState : AIState
{
    Animator bossAnimator;
    private bool called = false;

    float timer;
    bool canExit;
    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }

    public override void EnterState(BossAI ctx)
    {
        bossAnimator.SetTrigger("Staggered");
        canExit = false;

        float staggerTime = 4;
        timer = Time.time + staggerTime;
    }

    public override void ExecuteState(BossAI ctx)
    {
        if (timer < Time.time || canExit)
        {
            //Transiton to flee
            bossAnimator.SetTrigger("NotStaggered");
            ctx.SwitchState(States.WalkState);
            return;
        }
    }

    public override void ExitState(BossAI ctx)
    {
        Debug.Log("Exiting stagger state");
    }

    public override void InitalizeState(BossAI ctx)
    {
        bossAnimator = bossTransform.GetComponent<Animator>();
    }

    public void NoLongerStaggered(FloatEvent ctx)
    {
        Debug.Log("Got out of stagger");
        canExit = true;
    }
}
