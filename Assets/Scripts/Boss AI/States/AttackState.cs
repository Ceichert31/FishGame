using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;


[CreateAssetMenu(fileName = "AttackState", menuName = "BossStates/Attack")]
public class AttackState : AIState, IAttackState
{
    [SerializeField] List<string> attacks = new List<string>();

    [SerializeField] Animator bossAnimator;

    string currentAttack;

    bool attacking;

    private bool called = false;

    float maxDistance;

    protected override bool Called 
    {
        get { return called; }
        set { called = value;  } 
    }

    public override void InitalizeState(BossAI ctx)
    {
        bossAnimator = bossTransform.GetComponent<Animator>();

        if (attacks.Count == 0)
        {
            throw new System.Exception("I has no attacks :(");
        }
        Debug.Log("EnteredAttack");

        maxDistance = 10;
    }


    public override void EnterState(BossAI ctx)
    {
        if(bossAnimator == null)
        {
            throw new System.Exception("You did not initalize");
        }
        GenerateAttack();
        ExecuteAttack();
    }

    public override void ExecuteState(BossAI ctx)
    {
        if(attacking)
        {
            return;
        }

        ctx.SwitchState(States.WalkState);
    }

    public override void ExitState(BossAI ctx)
    {

    }

    

    void GenerateAttack()
    {
        currentAttack = attacks[Random.Range(0, attacks.Count)];
    }

    void ExecuteAttack()
    {
        attacking = true;
        Debug.Log(currentAttack);
        bossAnimator.SetTrigger(currentAttack);
    }

    public bool Attacking
    {
        get { return attacking; }
        set { attacking = value; }
    }
}
