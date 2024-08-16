using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the rigidboddies velocity to it's forward times some speed
/// </summary>
public class ProjectileBehavior : MonoBehaviour
{
    Rigidbody rb;
    float speed = 15;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    public void Parried(Vector3 forward)
    {
        transform.forward = forward;
        speed *= 2;
    }
}
