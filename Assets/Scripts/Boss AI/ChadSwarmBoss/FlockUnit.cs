using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

public class FlockUnit : MonoBehaviour
{
    [SerializeField] float FOVAngle;
    [SerializeField] float smoothDamp;
    [SerializeField] LayerMask obsticleLayer;

    Vector3 currentVelocity;

    float speed;

    private List<FlockUnit> cohesionNeighbors = new List<FlockUnit>();
    private List<FlockUnit> avoidanceNeighbors = new List<FlockUnit>();
    private List<FlockUnit> allignmentNeighbors = new List<FlockUnit>();


    Flock assignedFlock;

    public Transform myTransform { get; set; }
    public int assignedVert;

    private void Start()
    {
        myTransform = transform;
    }

    public void InitalizeSpeed(float _speed)
    {
        speed = _speed;
    }

    public void AssignFlock(Flock flock)
    {
        assignedFlock = flock;
    }

    public void MoveUnit()
    {
        FindNeighboringUnits();
        CalculateSpeed();
        Vector3 cohesionVector = CalculateCohesionVector() * assignedFlock.CohesionWeight;
        Vector3 avoidanceVector = CalculateAvoidanceVector() * assignedFlock.AvoidanceWeight;
        Vector3 allignmentVector = CalculateAllignmentVector() * assignedFlock.AllignmentWeight;
        Vector3 boundsVector = CalculateBoundsVector() * assignedFlock.BoundsWeight;
/*        Vector3 obsticleAvoidance = CalculateObsticleAvoidance() * assignedFlock.ObsticleAvoidanceWeight;
*/
        Vector3 moveVector = cohesionVector + avoidanceVector + allignmentVector + boundsVector;

        moveVector = Vector3.SmoothDamp(myTransform.forward, moveVector, ref currentVelocity, smoothDamp);
        moveVector = moveVector.normalized * speed;

        if(moveVector == Vector3.zero)
        {
            moveVector = transform.forward;
        }

        myTransform.forward = moveVector;
        myTransform.position += moveVector * Time.deltaTime;
    }

    

    private void FindNeighboringUnits()
    {
        cohesionNeighbors.Clear();
        avoidanceNeighbors.Clear();
        allignmentNeighbors.Clear();

        var allUnits = assignedFlock.allUnits;

        for(int i = 0; i < allUnits.Length; i++)
        {
            var currentUnit = allUnits[i];
            if(currentUnit != this)
            {
                //Might need to use square root and then square cohesion distance, change if failure
                var currentNeighborDistance = Vector3.Distance(myTransform.position, currentUnit.myTransform.position);

                if(currentNeighborDistance <= assignedFlock.CohesionDistance)
                {
                    cohesionNeighbors.Add(currentUnit);
                }
                if (currentNeighborDistance <= assignedFlock.AvoidanceDistance)
                {
                    avoidanceNeighbors.Add(currentUnit);
                }
                if (currentNeighborDistance <= assignedFlock.AllignmentDistance)
                {
                    allignmentNeighbors.Add(currentUnit);
                }

            }
        }
    }

    private Vector3 CalculateCohesionVector()
    {
        var cohesionVector = Vector3.zero;
        int neighborsInPOV = 0;

        if(cohesionNeighbors.Count == 0)
        {
            return cohesionVector;
        }

        for(int i = 0; i < cohesionNeighbors.Count; i++)
        {
            if (IsInFOV(cohesionNeighbors[i].myTransform.position))
            {
                neighborsInPOV++;
                cohesionVector += cohesionNeighbors[i].myTransform.position;
            }
        }

        cohesionVector /= neighborsInPOV;

        cohesionVector -= myTransform.position;


        return cohesionVector.normalized;
    }



    private Vector3 CalculateAvoidanceVector()
    {
        Vector3 avoidanceVector = Vector3.zero;
        if (avoidanceNeighbors.Count == 0)
        {
            return avoidanceVector;
        }

        int neighborsInPOV = 0;

        for (int i = 0; i < avoidanceNeighbors.Count; i++)
        {
            if (IsInFOV(avoidanceNeighbors[i].myTransform.position))
            {
                neighborsInPOV++;
                avoidanceVector += myTransform.position - avoidanceNeighbors[i].myTransform.position;
            }
        }

        avoidanceVector /= neighborsInPOV;
        return avoidanceVector.normalized;
    }

    private Vector3 CalculateAllignmentVector()
    {
        Vector3 allignmentVector = myTransform.forward;
        if (allignmentNeighbors.Count == 0)
        {
            return allignmentVector;
        }

        int neighborsInPOV = 0;

        for(int i = 0; i < allignmentNeighbors.Count; i++)
        {
            if (IsInFOV(cohesionNeighbors[i].myTransform.position))
            {
                neighborsInPOV++;
                allignmentVector += cohesionNeighbors[i].myTransform.forward;
            }
        }
        allignmentVector /= neighborsInPOV;

        allignmentVector -= myTransform.forward;



        return allignmentVector.normalized;
    }



    private Vector3 CalculateBoundsVector()
    {
        Vector3 offsetToCenter = assignedFlock.transform.position - myTransform.position;

        //magic number here
        bool isNearCenter = offsetToCenter.magnitude >= assignedFlock.BoundsDistance * .9f;

        return isNearCenter ? offsetToCenter.normalized : Vector3.zero;
    }

    /*private Vector3 CalculateObsticleAvoidance()
    {
        Vector3 obsticleAvoidanceVector = Vector3.zero;
        RaycastHit hit;

        if(Physics.Raycast())
    }*/




    //maybe look to replace with dot product
    private bool IsInFOV(Vector3 position)
    {
        return Vector3.Angle(myTransform.forward, position - myTransform.position) <= FOVAngle;
    }

    private void CalculateSpeed()
    {
        if(cohesionNeighbors.Count == 0)
        {
            return;
        }

        float totalSpeed = 0;

        for(int i = 0; i < cohesionNeighbors.Count; i++)
        {
            totalSpeed += cohesionNeighbors[i].speed;
        }

        totalSpeed /= cohesionNeighbors.Count;

        speed = totalSpeed;

        speed = Mathf.Clamp(speed, assignedFlock.MinSpeed, assignedFlock.MaxSpeed);
    }
}
