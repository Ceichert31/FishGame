using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTracker : MonoBehaviour
{
    Transform objectTransform;
    Vector3 previousPosition;
    Vector3 objectDirection;

    private void Awake()
    {
        objectTransform = transform.parent;
        previousPosition = objectTransform.position;
    }

    private void Update()
    {
        // Calculate the direction of movement in the world space
        objectDirection = objectTransform.position - previousPosition;

        // Update the previous position for the next frame
        previousPosition = objectTransform.position;
    }

    public Vector3 ObjectDirection
    {
        get
        {
            // Convert world space movement direction to local space
            Vector3 localDirection = transform.InverseTransformDirection(objectDirection);
            return new Vector3(localDirection.y, localDirection.x, 0).normalized;
        }
    }
}