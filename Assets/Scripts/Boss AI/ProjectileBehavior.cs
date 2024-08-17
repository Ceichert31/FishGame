using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the rigidboddies velocity to it's forward times some speed
/// </summary>
public class ProjectileBehavior : MonoBehaviour, IProjectile
{
    Rigidbody rb;

    float speed = 20f;

    private Transform PlayerPosition => GameManager.Instance.Player.transform;

    private Vector3 targetDirection;

    public bool IsParried { get => isParried;}

    private bool isParried;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        targetDirection = (PlayerPosition.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = targetDirection * speed;
    }

    public void Parried(Vector3 forward)
    {
        isParried = true;

        targetDirection = forward;

        speed *= 2;
    }
}

public interface IProjectile
{
    public bool IsParried { get; }
    public void Parried(Vector3 foward);
}
