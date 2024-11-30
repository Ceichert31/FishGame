using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakpoint : MonoBehaviour
{
    [Header("Weakpoint Settings")]
    [SerializeField] private float weakpointDamageMultiplier = 1.5f; 

    private BossHealth health;
    private FloatEvent damage;
    void Awake()
    {
        health = transform.parent.GetComponent<BossHealth>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If collision object is parried projectile, deal damage
        if (collision.gameObject.TryGetComponent(out IProjectile projectileInstance))
        {
            //If it has already delt damage, don't deal damage again
            if (projectileInstance.IsParried) return;

            //Cache projectile damage
            damage.FloatValue = projectileInstance.ProjectileDamage;

            //Deal parry damage
            health.UpdateHealth(damage);

            //Parent projectile to this
            collision.gameObject.transform.parent = transform.parent;

            projectileInstance.DeleteProjectile();

            //Get collsion point and play special effects
        }
    }
}
