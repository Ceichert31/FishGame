using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

[CreateAssetMenu(fileName ="WalkState", menuName ="BossStates/Walk")]
public class WalkState : AIState
{
    [SerializeField] LureProjectileSpawner projectileSpawner;
    float maxDistance;
    float fireTime;

    [Header("Variables for controlling unique movement")]
    [SerializeField] float initalMoveAmmount = 10;
    [SerializeField] float slowDownAmmount = 2;
    float timeUntilNextMovement = 1;
    float currentTime;
    float currentMoveAmmount;

    private bool called = false;

    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }

    public override void InitalizeState(BossAI ctx)
    {
        ctx.Agent.speed = 5f;
        projectileSpawner = ctx.GetComponent<LureProjectileSpawner>();
        currentTime = timeUntilNextMovement;
        currentMoveAmmount = initalMoveAmmount;
        maxDistance = 20;
    }


    public override void EnterState(BossAI ctx)
    {

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
        bossTransform.LookAt(new Vector3(Player.x, bossTransform.position.y, Player.z));
        //Creates a unique movement pattern simmilar to what u would see on some fishing lure
        if(currentMoveAmmount <= 0)
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
        
    }


}
