using HelperMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "BossStates/Tree Attack")]
public class TreeAttackState : AIState
{
    [SerializeField] List<string> attacks = new List<string>();

    [SerializeField] Animator bossAnimator;

    string currentAttack;

    bool attacking;

    private bool called = false;

    float maxDistance;

    [Header("Variables for controlling unique movement")]
    [SerializeField] float initalMoveAmmount = 0;
    [SerializeField] float slowDownAmmount = 0;
    float timeUntilNextMovement;
    float currentTime;
    float currentMoveAmmount;
    [SerializeField] float rotationSpeed = 2;

    protected override bool Called
    {
        get { return called; }
        set { called = value; }
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

        timeUntilNextMovement = 1;
        currentTime = timeUntilNextMovement;
        currentMoveAmmount = initalMoveAmmount;
        maxDistance = 10;

        maxDistance = 10;
    }


    public override void EnterState(BossAI ctx)
    {
        if (bossAnimator == null)
        {
            throw new System.Exception("You did not initalize");
        }
        GenerateAttack(0);
        ExecuteAttack();
    }

    public override void ExecuteState(BossAI ctx)
    {
        MoveBehavior();

        if (attacking)
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

        if (Util.DistanceNoY(Player, bossTransform.position) < maxDistance)
        {
            GenerateAttack(1);
            ExecuteAttack();

        }

        ctx.SwitchState(States.WalkState);

        //Peach Cobler is alright

        //Had better at a school cafeteria
    }

    void MoveBehavior()
    {
        //Temp LookAtSolution
        //bossTransform.LookAt();

        //Slowly rotates the boss to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(Util.VectorNoY(Player) - Util.VectorNoY(bossTransform.position));
        bossTransform.rotation = Quaternion.Slerp(bossTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //Creates a unique movement pattern simmilar to what u would see on some fishing lure
        if (currentMoveAmmount <= 0)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = timeUntilNextMovement;
                currentMoveAmmount = initalMoveAmmount;
            }

            return;
        }

        bossTransform.position += bossTransform.forward * currentMoveAmmount * Time.deltaTime;

        currentMoveAmmount -= slowDownAmmount * Time.deltaTime;
    }

    public override void ExitState(BossAI ctx)
    {

    }



    void GenerateAttack(int attackNum)
    {
        currentAttack = attacks[attackNum];
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
