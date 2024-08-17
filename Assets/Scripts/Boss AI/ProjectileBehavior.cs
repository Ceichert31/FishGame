using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the rigidboddies velocity to it's forward times some speed
/// </summary>
public class ProjectileBehavior : MonoBehaviour, IProjectile
{
    [SerializeField] private float projectileDamage = 5f;

    [SerializeField] private float projectileSpeed = 20f;

    [SerializeField] private float projectileLifetime = 6f;

    private Rigidbody rb;

    private bool isParried;

    private Transform PlayerPosition => GameManager.Instance.Player.transform;

    private Vector3 targetDirection;

    private float currentTime;

    public bool IsParried { get => isParried;}
    public float ProjectileDamage { get => projectileDamage; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        targetDirection = (PlayerPosition.position - transform.position).normalized;
    }

    private void Update()
    {
        currentTime = Time.time + projectileLifetime;

        if (Time.time > currentTime)
            DisableProjectile();
    }

    private void FixedUpdate()
    {
        rb.velocity = targetDirection * projectileSpeed;
    }

    public void Parried(Vector3 forward)
    {
        isParried = true;

        targetDirection = forward;

        projectileSpeed *= 2;
    }

    public void DisableProjectile() => gameObject.SetActive(false);
}

public interface IProjectile
{
    public float ProjectileDamage { get; }
    public bool IsParried { get; }
    public void Parried(Vector3 foward);
    public void DisableProjectile();
}
