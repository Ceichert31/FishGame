using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

//TODO:
//Move Walk and look Behavior to Composition Class
[RequireComponent(typeof(BossLookBehavior))]
[CreateAssetMenu(fileName ="WalkState", menuName ="BossStates/Walk")]
public class WalkState : AIState
{
    [SerializeField] IProjectileSpawner projectileSpawner;
    [SerializeField] IBossWalkBehavior walkBehavior;
    [SerializeField] IBossLookAtPlayer lookBehavior;
    float maxDistance = 10;
    float fireTime;
    float walkStateTimer;
    [SerializeField] float 
        waitAmmount = 10,
        projectileFireWaitTime = 3;

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
    }

    public override void ExecuteState(BossAI ctx)
    {
        walkBehavior.MoveBehavior();
        lookBehavior.LookAtPlayer();

        if (Util.CheckTimer(walkStateTimer))
        {
            ctx.SwitchState(States.AttackState);
        }

        //Temp exit condition to make combat not feel bad
        if (Util.DistanceNoY(Player,bossTransform.position) < maxDistance)
        {
            ctx.SwitchState(States.AttackState);
        }
        /*
         * Walking state code:
         * Ok so this should be simmilar to idle in terms of a more of an in between attacks gap closer type behvior
         * Maybe have the boss causally fire projectile patters as he walks toward the player for physical attacks
         * 
         * Exit Condition:
         * We become in range of the player to do whatever out next attack is
         * We get successflly grappled and mounted
         */

        //Exit Condition(temps)


        //ProjectileTesting *For Testing A Consistent Firing Pattern
        if (fireTime <= 0)
        {
            float waitTime = projectileFireWaitTime;
            projectileSpawner.Spawn(5);
            fireTime = waitTime;
        }
        //In the future might want to make this unscalled to not get messed with by time discrepincies
        fireTime -= Time.deltaTime;

        //SpawnProjectilesBetweenTheSpawnPoints
        //projectileSpawner
    }

    public override void ExitState(BossAI ctx)
    {
        //Stops all projectile spawning whenever the state is left
        projectileSpawner.StopSpawning();
        fireTime = 1;
    }
}

//CompositionInterfaces
interface IBossWalkBehavior
{
    void MoveBehavior();
}

interface IBossLookAtPlayer
{
    void LookAtPlayer();
}
