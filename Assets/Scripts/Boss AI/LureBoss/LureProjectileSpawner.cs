using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureProjectileSpawner : ProjectileManager
{
    [SerializeField] Transform defaultSpawnLocation;
    [SerializeField] int defaultProjectileAmmount;
    private void Start()
    {
        OnStart();
    }

    [ContextMenu("SpawnObjects")]
    void DevSpawnObjects()
    {
        InitalizeProjectileSpawner(defaultProjectileAmmount, 1, defaultSpawnLocation);
    }

    public override IEnumerator SpawnObjects()
    {
        for (int i = 0; i < projectileAmmount; i++)
        {
            pool.Get();
            yield return wfs;
        }
    }
}
