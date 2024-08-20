using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FleeState", menuName = "BossStates/Flee")]
public class FleeState : AIState
{
    Vector3 startLocation;
    Vector3 fleeLocation;
    Animator bossAnimator;
    LureProjectileSpawner lureProjectileSpawner;

    float fleeTime;
    float fleeDistance;
    float timer;

    bool called = false;

    private Vector3[] directions;
    private List<Vector3> projectileSpawnLocations;

    int projectilesToSpawn;
    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }


    public override void InitalizeState(BossAI ctx)
    {
        directions = new Vector3[2] { Vector3.right, Vector3.forward };
        bossAnimator = bossTransform.GetComponent<Animator>();
        lureProjectileSpawner = bossTransform.GetComponent<LureProjectileSpawner>();
        fleeTime = 5f;
        fleeDistance = 20;
        projectilesToSpawn = 5;
    }

    public override void EnterState(BossAI ctx)
    {
        startLocation = bossTransform.position;
        timer = Time.time + fleeTime;
    }

    public override void ExecuteState(BossAI ctx)
    {
        if(timer < Time.time)
        {
            bossAnimator.SetTrigger("Resurface");
            ctx.SwitchState(States.IdleState);
        }
    }

    public override void ExitState(BossAI ctx)
    {
        
    }
    /// <summary>
    /// Generates a random int(-1,0, or 1)
    /// </summary>
    int PosNegPicker()
    {
        return Random.Range(-1, 2);
    }

    /// <summary>
    /// Generates a random direction(either forward or right)
    /// </summary>
    /// <returns></returns>
    Vector3 RandomDirection()
    {
        return directions[Random.Range(0, 2)];
    }

    /// <summary>
    /// Teleports the fish in a random direction
    /// </summary>
    public void TeleportFish()
    {
        fleeLocation = (RandomDirection() * PosNegPicker() * fleeDistance) + bossTransform.position;
        bossTransform.position = fleeLocation;
    }

    /*public void SpawnProjectiles()
    {

        projectileSpawnLocations.Clear();
        for(int i = 0; i < projectilesToSpawn; i++)
        {

        }
    }*/

    /*Vector3 ProjectileSpawnLocation()
    {
        Vector3 directionBetween = fleeLocation - startLocation;
        return 
    }*/
}
