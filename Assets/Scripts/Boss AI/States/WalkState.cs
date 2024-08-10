using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;
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
    public override void EnterState(BossAI ctx)
    {
        //Temp Solution
        bossTransform = transform.parent.parent;


        ctx.Agent.speed = 5f;
        currentTime = timeUntilNextMovement;
        currentMoveAmmount = initalMoveAmmount;
        
        //Play idle animation
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
        if(Util.DistanceNoY(Player, transform.position) < maxDistance)
        {
            ctx.SwitchState(ctx.idleState);
            Debug.Log("Close Enough");
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
        bossTransform.LookAt(Player);
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
