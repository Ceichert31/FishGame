using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTracker : MonoBehaviour
{
    Transform playerTransform;
    Vector3 previousPosition;
    Vector3 playerDirection;
    // Start is called before the first frame update

    private void Awake()
    {
        playerTransform = transform.parent;
        previousPosition = playerTransform.position;
    }

    private void Update()
    {
        playerDirection = playerTransform.position - previousPosition;
        previousPosition = playerTransform.position;
    }

    public Vector3 PlayerDirection
    {
        get { return new Vector3(playerDirection.x, -playerDirection.z, 0).normalized; }
    }
}
