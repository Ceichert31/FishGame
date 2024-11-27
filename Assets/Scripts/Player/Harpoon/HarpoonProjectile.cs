using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HarpoonProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float projectileDamage = 5f;

    [SerializeField] private float projectileSpeed = 20f;

    [SerializeField] private float projectileLifetime = 6f;

    private Vector3 targetDirection;

    private float currentTime;

    private Rigidbody rb;

    public void Init(float damage, float speed, float lifetime, Vector3 direction)
    {
        projectileDamage = damage;
        projectileSpeed = speed;
        projectileLifetime = lifetime;
        targetDirection = direction;
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();

        currentTime = Time.time + projectileLifetime;
    }
    private void FixedUpdate()
    {
        rb.velocity = targetDirection * projectileSpeed;
    }
    private void Update()
    {
        if (Time.time > currentTime)
            DeleteProjectile();
    }

    //Interface implementation
    public bool IsParried => false;

    public float ProjectileDamage => projectileDamage;

    public void DeleteProjectile() => Destroy(gameObject);
}
