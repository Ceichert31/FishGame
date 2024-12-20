using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;


[CreateAssetMenu(fileName = "AttackState", menuName = "BossStates/Attack")]
public class AttackState : AIState, IAttackState
{
    [SerializeField] Animator bossAnimator;

    string currentAttack;

    bool attacking;

    private bool called = false;

    protected override bool Called 
    {
        get { return called; }
        set { called = value;  } 
    }

    public override void InitalizeState(BossAI ctx)
    {
        bossAnimator = bossTransform.GetComponent<Animator>();

        if (ctx.BossInformation.meleeAttacks.Count == 0)
        {
            throw new System.Exception("I has no attacks :(");
        }
        Debug.Log("EnteredAttack");
    }


    public override void EnterState(BossAI ctx)
    {
        if(bossAnimator == null)
        {
            throw new System.Exception("You did not initalize");
        }

        /*if(Util.DistanceNoY(bossTransform.position, Player) < maxDistance)
        {
            GenerateAttack(physicalAttacks);
        }
        else
        {
            GenerateAttack(rangedAttacks);
        }*/

        float temp = Util.DistanceNoY(bossTransform.position, Player);
        Debug.Log(temp);
        //How we rockin?
        GenerateAttack(Util.DistanceNoY(bossTransform.position, Player) <= ctx.BossInformation.meleeDistance ? ctx.BossInformation.meleeAttacks : ctx.BossInformation.rangedAttacks);
        ExecuteAttack();

        //GenerateAttack(physicalAttacks);
        //ExecuteAttack();
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
        attacking = false;
    }

    void GenerateAttack(List<string> attacks)
    {
        currentAttack = attacks[Random.Range(0, attacks.Count - 1)];
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
