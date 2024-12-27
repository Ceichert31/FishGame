using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the rigidboddies velocity to it's forward times some speed
/// </summary>
public class HomingProjectileBehavior : MonoBehaviour, IProjectile
{
    [SerializeField] private float projectileDamage = 5f;

    [SerializeField] private float stopTrackingDistance = 5f;

    [SerializeField] private float projectileLifetime = 6f;

    [SerializeField] private AnimationCurve speedCurve;

    private Rigidbody rb;

    private bool canTrack = true;

    private Transform PlayerPosition => GameManager.Instance.Player.transform;

    private Vector3 targetDirection;

    private float currentTime;

    //Interface Variables

    public bool IsParried => false;

    public float ParryAmount => projectileDamage;

    float IProjectile.ProjectileDamage => projectileDamage;

    public int ParryType => (int)ParryTypes.ProjectileParry;


    public HomingProjectileBehavior(float damage, float lifetime)
    {
        projectileDamage = damage;
        projectileLifetime = lifetime;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        targetDirection = (PlayerPosition.position - transform.position).normalized;

        canTrack = true;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (canTrack)
            targetDirection = (PlayerPosition.position - transform.position).normalized;

        //Stop tracking when within certain distance
        if (Vector3.Distance(PlayerPosition.position, transform.position) <= stopTrackingDistance)
            canTrack = false;

        if (currentTime > projectileLifetime)
            DeleteProjectile();
    }

    private void FixedUpdate()
    {
        rb.velocity = targetDirection * speedCurve.Evaluate(currentTime);
    }

    public void DeleteProjectile() => Destroy(gameObject);
}
