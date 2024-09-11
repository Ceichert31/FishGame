using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the rigidboddies velocity to it's forward times some speed
/// </summary>
public class ProjectileBehavior : MonoBehaviour, IProjectile, IParryable
{
    [SerializeField] private float projectileDamage = 5f;

    [SerializeField] private float projectileSpeed = 20f;

    [SerializeField] private float projectileLifetime = 6f;

    private Rigidbody rb;

    private bool isParried;

    private Transform PlayerPosition => GameManager.Instance.Player.transform;

    private Vector3 targetDirection;

    private float currentTime;

    //Interface Variables

    bool IParryable.Parried => isParried;

    public bool IsParried => IsParried;

    public float ParryAmount => projectileDamage;

    float IProjectile.ProjectileDamage => projectileDamage;

    public int ParryType => (int)ParryTypes.ProjectileParry;


    public ProjectileBehavior(float damage, float speed, float lifetime)
    {
        projectileDamage = damage;
        projectileSpeed = speed;
        projectileLifetime = lifetime;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        targetDirection = (PlayerPosition.position - transform.position).normalized;

        currentTime = Time.time + projectileLifetime;
    }

    private void Update()
    {
        if (Time.time > currentTime)
            DeleteProjectile();
    }

    private void FixedUpdate()
    {
        rb.velocity = targetDirection * projectileSpeed;
    }

    public void DeleteProjectile() => Destroy(gameObject);

    public void OnParry()
    {
        if(isParried)
        {
            return;
        }    

        isParried = true;

        targetDirection = Camera.main.transform.forward;

        projectileSpeed *= 2;

        currentTime = Time.time + projectileLifetime;
    }
}

/// <summary>
/// Location: Projectile Behavior
/// 
/// </summary>
public interface IProjectile
{
    public bool IsParried { get; }
    public float ProjectileDamage { get; }
    public void DeleteProjectile();
}
