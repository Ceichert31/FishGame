using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureProjectileSpawner : ProjectileManager
{
    [SerializeField] Transform defaultSpawnLocation;
    [SerializeField] int defaultProjectileAmmount;
    [SerializeField] GameObject specialProjectile;
    private void Start()
    {
        OnStart();
    }

    [ContextMenu("SpawnObjects")]
    void DevSpawnObjects()
    {
        InitalizeProjectileSpawner(null, defaultProjectileAmmount, 1, defaultSpawnLocation);
    }

    [ContextMenu("SpawnSpecialObjects")]
    void DevSpawnSpecialObject()
    {
        InitalizeProjectileSpawner(specialProjectile, defaultProjectileAmmount, 1, defaultSpawnLocation);
    }

    public override IEnumerator SpawnProjectiles(GameObject projectile, int _projectileAmmount)
    {
        if (projectile != null)
        {
            for (int i = 0; i < _projectileAmmount; i++)
            {
                Instantiate(projectile, spefSpawnLocation.position, Quaternion.identity);
                yield return wfs;
            }
        }
        else
        {
            for (int i = 0; i < _projectileAmmount; i++)
            {
                pool.Get();
                yield return wfs;
            }
        }
    }
}
