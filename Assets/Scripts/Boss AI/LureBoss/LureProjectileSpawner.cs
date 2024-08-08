using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureProjectileSpawner : ProjectileManager
{
    Vector3 playerPosition => GameManager.Instance.Player.transform.position;

    [SerializeField] Transform defaultSpawnLocation;
    [SerializeField] int defaultProjectileAmmount;
    [SerializeField] GameObject specialProjectile;

    [SerializeField] List<Transform> projectileSpawnPoints;
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
                GameObject instance = pool.Get();
                instance.transform.LookAt(playerPosition);
                yield return wfs;
            }
        }
    }

    //PatternSpawning
    public void SpawnPattern(float timeInbetween)
    {
        StartCoroutine(PatternSpawner(timeInbetween));
    }

    IEnumerator PatternSpawner(float timeInbetween)
    {
        for (int i = 0; i < projectileSpawnPoints.Count; i++)
        {
            InitalizeProjectileSpawner(null, 1, timeInbetween, projectileSpawnPoints[i]);
            yield return wfs;
        }
    }

    public int SpawnLocationCount
    {
        get { return projectileSpawnPoints.Count; }
    }

}
