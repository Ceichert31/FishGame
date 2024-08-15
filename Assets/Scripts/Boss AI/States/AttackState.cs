using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

[CreateAssetMenu(fileName = "AttackState", menuName = "BossStates/Attack")]
public class AttackState : AIState
{
    [SerializeField] List<string> attacks = new List<string>();

    Animator bossAnimator;

    string currentAttack;

    bool attacking;
    public override void InitalizeState(BossAI ctx)
    {
        bossTransform = Util.TryGetParent(ctx.transform);
        bossAnimator = bossTransform.GetComponent<Animator>();
        

        for(int i = 0; i < bossAnimator.parameters.Length; i++)
        {
            attacks[i] = bossAnimator.parameters[i].name;
        }

        if (attacks.Count == 0)
        {
            throw new System.Exception("I has no attacks :(");
        }
        Debug.Log("EnteredAttack");
    }


    public override void EnterState(BossAI ctx)
    {
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


        //Peach Cobler is alright

        //Had better at a school cafeteria
    }

    public override void ExitState(BossAI ctx)
    {

    }

    

    void GenerateAttack()
    {
        currentAttack = attacks[Random.Range(0, attacks.Count)];
        currentAttack = "Spin";
    }

    void ExecuteAttack()
    {
        attacking = true;
        Debug.Log(currentAttack);
        bossAnimator.SetTrigger(currentAttack);
    }
}
