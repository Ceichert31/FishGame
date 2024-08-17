using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the rigidboddies velocity to it's forward times some speed
/// </summary>
public class ProjectileBehavior : MonoBehaviour, IProjectile
{
    Rigidbody rb;
    float speed = 15;

    public bool IsParried { get => isParried;}

    private bool isParried;

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
        isParried = true;

        transform.forward = forward;

        speed *= 2;
    }
}

public interface IProjectile
{
    public bool IsParried { get; }
    public void Parried(Vector3 foward);
}
