using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

[CreateAssetMenu(fileName = "AttackState", menuName = "BossStates/Attack")]
public class AttackState : AIState
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

        /*for(int i = 0; i < bossAnimator.parameters.Length; i++)
        {
            attacks[i] = bossAnimator.parameters[i].name;
        }*/

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
        //Distance Check with player

        //if()
        //If the player walks away far
        /*if(Util.DistanceNoY(bossTransform.position, Player) > maxDistance)
        {
            ctx.SwitchState(States.WalkState);
        }*/
        ctx.SwitchState(States.IdleState);

        //Peach Cobler is alright

        //Had better at a school cafeteria
    }

    public override void ExitState(BossAI ctx)
    {

    }

    

    void GenerateAttack()
    {
        currentAttack = attacks[Random.Range(0, attacks.Count)];
        //currentAttack = "Spin";
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
