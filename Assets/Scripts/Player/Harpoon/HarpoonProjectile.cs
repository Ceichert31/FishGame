using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HarpoonProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float projectileDamage => GameManager.Instance.PlayerDamage;

    [SerializeField] private float projectileSpeed = 20f;

    [SerializeField] private float projectileLifetime = 6f;

    [SerializeField] private int damageLayer = 8;
    [SerializeField] private int weakPointLayer = 9;

    private Vector3 targetDirection;

    private float currentTime;

    private Rigidbody rb;

    private bool cannotDestroy;

    public void Init(float speed, float lifetime, Vector3 direction)
    {
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
        if (cannotDestroy) return;

        rb.velocity = targetDirection * projectileSpeed;
    }
    private void Update()
    {
        if (Time.time > currentTime && !cannotDestroy)
            Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (cannotDestroy) return;
        if (collision.gameObject.layer != damageLayer) Destroy(gameObject);
    }

    //Interface implementation
    public bool IsParried => cannotDestroy;

    public float ProjectileDamage => projectileDamage;

    public void DeleteProjectile()
    {
        rb.velocity = Vector3.zero;
        cannotDestroy = true;
        rb.isKinematic = true;
    }
}