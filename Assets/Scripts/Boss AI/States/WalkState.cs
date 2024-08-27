using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

[CreateAssetMenu(fileName ="WalkState", menuName ="BossStates/Walk")]
public class WalkState : AIState
{
    [SerializeField] BossProjectileSpawner projectileSpawner;
    float maxDistance;
    float fireTime;

    [Header("Variables for controlling unique movement")]
    [SerializeField] float initalMoveAmmount;
    [SerializeField] float slowDownAmmount;
    float timeUntilNextMovement;
    float currentTime;
    float currentMoveAmmount;
    float rotationSpeed;

    private bool called = false;

    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }

    public override void InitalizeState(BossAI ctx)
    {
        projectileSpawner = ctx.GetComponent<BossProjectileSpawner>();
        initalMoveAmmount = 10;
        slowDownAmmount = 2;
        timeUntilNextMovement = 1;
        currentTime = timeUntilNextMovement;
        currentMoveAmmount = initalMoveAmmount;
        maxDistance = 10;
        rotationSpeed = 1;
    }


    public override void EnterState(BossAI ctx)
    {
        ctx.Agent.speed = 5f;
    }

    public override void ExecuteState(BossAI ctx)
    {
        //MoveCode:
        MoveBehavior();

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
        if(Util.DistanceNoY(Player, bossTransform.position) < maxDistance)
        {
            ctx.SwitchState(States.AttackState);
        }

        //ProjectileTesting *For Testing A Consistent Firing Pattern
        if (fireTime <= 0)
        {
            float waitTime = 2;
            projectileSpawner.SpawnPattern(waitTime);
            fireTime = waitTime * projectileSpawner.SpawnLocationCount;
        }
        //In the future might want to make this unscalled to not get messed with by time discrepincies
        fireTime -= Time.deltaTime;

        //SpawnProjectilesBetweenTheSpawnPoints
        //projectileSpawner
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
            if(currentTime > 0)
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
        //Stops all projectile spawning whenever the state is left
        projectileSpawner.StopProjectileSpawner();
        fireTime = 1;
    }


}
