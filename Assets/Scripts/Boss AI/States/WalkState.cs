using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

//TODO:
//Move Walk and look Behavior to Composition Class
[RequireComponent(typeof(BossLookBehavior))]
[CreateAssetMenu(fileName = "WalkState", menuName = "BossStates/Walk")]
public class WalkState : AIState
{
    [SerializeField] IProjectileSpawner projectileSpawner;
    [SerializeField] IBossWalkBehavior walkBehavior;
    [SerializeField] IBossLookAtPlayer lookBehavior;
    float fireTime;
    float walkStateTimer;
    [SerializeField]
    float
        waitAmmount = 10,
        projectileFireWaitTime = 3,
        gracePeriod = 2;

    float gracePeriodTimer;
    private bool called = false;

    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }

    public override void InitalizeState(BossAI ctx)
    {
        projectileSpawner = ctx.GetComponent<IProjectileSpawner>();
        lookBehavior = ctx.GetComponent<IBossLookAtPlayer>();

        try
        {
            walkBehavior = ctx.GetComponent<IBossWalkBehavior>();
        }
        catch
        {
            Debug.LogError("This boss requires a move behavior script");
        }

    }


    public override void EnterState(BossAI ctx)
    {
        walkStateTimer = waitAmmount + Time.time;

        gracePeriodTimer = gracePeriod + Time.time;
    }

    public override void ExecuteState(BossAI ctx)
    {
        //Method that checks if we are able to change our current state
        CanChangeState(ctx);

        //Look At Player And Move Toward Player
        walkBehavior.MoveBehavior();
        lookBehavior.LookAtPlayer();


        //Projectile Firing
        if (fireTime <= 0)
        {
            float waitTime = projectileFireWaitTime;
            //projectileSpawner.Spawn(5);
            fireTime = waitTime;
        }

        //In the future might want to make this unscalled to not get messed with by time discrepincies
        fireTime -= Time.deltaTime;
    }

    public override void ExitState(BossAI ctx)
    {
        //Stops all projectile spawning whenever the state is left
        projectileSpawner.StopSpawning();
        fireTime = 1;
    }

    void CanChangeState(BossAI ctx)
    {
        if(ctx.BossInformation.gracePeriod > Time.time)
        {
            return;
        }

        //State Changers
        if (Util.CheckTimer(ctx.BossInformation.waitTime))
        {
            ctx.SwitchState(States.AttackState);
        }

        //Temp exit condition to make combat not feel bad
        if (Util.DistanceNoY(Player, bossTransform.position) < ctx.BossInformation.meleeDistance)
        {
            ctx.SwitchState(States.AttackState);
        }

        //Check and allow boss to perform ranged attacks
        else if (Util.DistanceNoY(Player, bossTransform.position) > ctx.BossInformation.maxDistance)
        {
            Debug.Log("howw");
            ctx.SwitchState(States.AttackState);
        }
    }
}

//CompositionInterfaces
public interface IBossWalkBehavior
{
    void MoveBehavior();

    void TeleportBehavior();
}

public interface IBossLookAtPlayer
{
    void LookAtPlayer();
}
