using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [Header("Spawn Information")]
    [SerializeField] FlockUnit flockUnitPrefab;
    [SerializeField] int flockSize;
    [SerializeField] Vector3 spawnBounds;

    [Header("Speed Variables")]
    [Range(0f, 10f)]
    [SerializeField] float minSpeed = 1;
    [Range(0f, 10f)]
    [SerializeField] float maxSpeed = 5;

    public float MinSpeed
    {
        get { return minSpeed; }
    }

    public float MaxSpeed
    {
        get { return maxSpeed; }
    }


    [Header("Detection Distances")]
    [Range(0f, 10f)]
    [SerializeField] float cohesionDistance;

    public float CohesionDistance
    {
        get { return cohesionDistance; }
    }

    [Range(0f, 10f)]
    [SerializeField] float avoidanceDistance;

    public float AvoidanceDistance
    {
        get { return avoidanceDistance; }
    }

    [Range(0f, 10f)]
    [SerializeField] float allignmentDistance;

    public float AllignmentDistance
    {
        get { return allignmentDistance; }
    }

    [Range(0f, 100f)]
    [SerializeField] float boundsDistance;

    public float BoundsDistance
    {
        get { return boundsDistance; }
    }

    [Range(0f, 100f)]
    [SerializeField] float obsticleAvoidanceDistance;

    public float ObsticleAvoidanceDistance
    {
        get { return obsticleAvoidanceDistance; }
    }


    [Header("Behavior Weights")]
    [Range(0f, 10f)]
    [SerializeField] float cohesionWeight;

    public float CohesionWeight
    {
        get { return cohesionWeight; }
    }

    [Range(0f, 10f)]
    [SerializeField] float avoidanceWeight;

    public float AvoidanceWeight
    {
        get { return avoidanceWeight; }
    }

    [Range(0f, 10f)]
    [SerializeField] float allignmentWeight;

    public float AllignmentWeight
    {
        get { return allignmentWeight; }
    }

    [Range(0f, 10f)]
    [SerializeField] float boundsWeight;

    public float BoundsWeight
    {
        get { return boundsWeight; }
    }

    [Range(0f, 10f)]
    [SerializeField] float obsticleAvoidanceWeight;

    public float ObsticleAvoidanceWeight
    {
        get { return obsticleAvoidanceWeight; }
    }


    public FlockUnit[] allUnits { get; set; }

    [SerializeField] bool shouldFlock;


    public bool ShouldFlock { get => shouldFlock; set { shouldFlock = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        GenerateFlock();
    }

    private void Update()
    {
        if(!shouldFlock)
        {
            return;
        }

        for(int i = 0; i < flockSize; i++)
        {
            allUnits[i].MoveUnit();
        }
    }

    private void GenerateFlock()
    {
        allUnits = new FlockUnit[flockSize];

        for(int i = 0; i < flockSize; i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere;

            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition = transform.position + randomVector;
            var spawnRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, spawnRotation);
            allUnits[i].AssignFlock(this);
            allUnits[i].InitalizeSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
        }
    }
}
