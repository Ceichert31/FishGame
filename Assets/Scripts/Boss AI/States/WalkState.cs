using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;
public class WalkState : AIState
{
    [SerializeField] LureProjectileSpawner projectileSpawner;
    float maxDistance;
    float fireTime;
    public override void EnterState(BossAI ctx)
    {
        ctx.Agent.speed = 5f;

        //Play idle animation
    }

    public override void ExecuteState(BossAI ctx)
    {


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

    public override void ExitState(BossAI ctx)
    {
        
    }
}
