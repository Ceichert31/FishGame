using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;
using System;

[CreateAssetMenu(fileName = "FleeState", menuName = "BossStates/Flee")]
public class FleeState : AIState
{
    [SerializeField] AudioPitcherSO bossResurfaceAudio;

    Vector3 startLocation;
    Vector3 fleeLocation;
    Animator bossAnimator;
    //AudioSource bossAudio;
    IProjectileSpawner lureProjectileSpawner;

    float fleeTime;
    float fleeDistance;
    float timer;

    bool called = false;

    private Vector3[] directions = new Vector3[2] { Vector3.right, Vector3.forward}; 
    private int[] posNeg = new int[2] { -1, 1 };
    private List<Vector3> projectileSpawnLocations = new List<Vector3>();

    int projectilesToSpawn;
    protected override bool Called
    {
        get { return called; }
        set { called = value; }
    }


    public override void InitalizeState(BossAI ctx)
    {
        bossAnimator = bossTransform.GetComponent<Animator>();
        lureProjectileSpawner = ctx.GetComponent<IProjectileSpawner>();
        fleeTime = 5f;
        fleeDistance = 50;
        projectilesToSpawn = 5;
        Debug.Log("here");
    }

    public override void EnterState(BossAI ctx)
    {
        startLocation = bossTransform.position;
        timer = Time.time + fleeTime;
        Debug.Log("here");
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

        Debug.Log(fleeLocation);
        fleeLocation = Vector3.zero;
        bossTransform.LookAt(Util.VectorSameY(Player, bossTransform.position.y));
        bossAnimator.applyRootMotion = true;
        //bossResurfaceAudio.Play(bossAudio);
    }
    /// <summary>
    /// Generates a random int(-1 or 1)
    /// </summary>
    int PosNegPicker()
    {
        return UnityEngine.Random.value > 0.5f ? 1 : -1;
    }

    /// <summary>
    /// Generates a random direction(either forward or right)
    /// </summary>
    /// <returns></returns>
    Vector3 RandomDirection()
    {
        return UnityEngine.Random.value > 0.5f ? Vector3.forward : Vector3.right;
    }

    /// <summary>
    /// Teleports the fish in a random direction
    /// </summary>
    public void TeleportFish()
    {
        Debug.Log("Got to teleportFish");
        try
        {
            bossAnimator.applyRootMotion = false;
        }
        catch
        {
            Debug.Log("Got the bug");
        }
        Vector3 randomDirection = RandomDirection();
        float posNeg = PosNegPicker();

        // Calculate the flee location based on the random direction and the distance.
        fleeLocation = (randomDirection * posNeg * fleeDistance) + bossTransform.position;

        //Debug.Log(fleeLocation);

        // Teleport the boss to the new location.
        bossTransform.position = fleeLocation;

        Debug.Log(startLocation);

        SpawnProjectiles();
    }

    void SpawnProjectiles()
    {
        // Clear the list to ensure no old positions are retained.
        projectileSpawnLocations.Clear();

        // Calculate the direction from the starting location to the flee location.
        Vector3 projectileSpawnDistance = ProjectileSpawnLocation();

        // Loop through and generate positions for each projectile to be spawned.
        for (int i = 1; i <= projectilesToSpawn; i++)
        {
            // Calculate the spawn position by dividing the direction by 'i' to space out the projectiles.
            Vector3 spawnPosition = fleeLocation + (projectileSpawnDistance / i);

            // Log the spawn position for debugging purposes.
            //Debug.Log(spawnPosition);

            // Adjust the Y position to match the height of the bossTransform.
            // Util.VectorNoY removes any Y-component, while Vector3.up * bossTransform.position.y adds the Y height back.
            projectileSpawnLocations.Add(Util.VectorNoY(spawnPosition) + Vector3.up * bossTransform.position.y);
        }

        // Spawn the projectiles at the calculated positions using a custom pattern.
        //lureProjectileSpawner.SpawnCustomPattern(1f, projectileSpawnLocations);
        lureProjectileSpawner.Spawn(3);
    }

    Vector3 ProjectileSpawnLocation()
    {
        // Calculate and return the direction from the start location to the flee location.
        Vector3 directionBetween = fleeLocation - startLocation;
        return directionBetween;
    }
}
