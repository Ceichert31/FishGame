using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileSpawner : ProjectileManager
{
    Vector3 playerPosition => GameManager.Instance.Player.transform.position;

    [SerializeField] Transform defaultSpawnLocation;
    [SerializeField] int defaultProjectileAmmount;

    //Currently using this projectile for all casses
    [SerializeField] GameObject tempProjectile;

    [SerializeField] List<Transform> projectileSpawnPoints;
    private void Start()
    {
        OnStart();
    }

    /*[ContextMenu("SpawnObjects")]
    void DevSpawnObjects()
    {
        InitalizeProjectileSpawner(null, defaultProjectileAmmount, 1, defaultSpawnLocation);
    }*/

    [ContextMenu("SpawnSpecialObjects")]
    void DevSpawnSpecialObject()
    {
        InitalizeProjectileSpawner(tempProjectile, defaultProjectileAmmount, 1, defaultSpawnLocation.position);
    }

    public override IEnumerator SpawnProjectiles(GameObject projectile, int _projectileAmmount, Vector3 specificSpawnLocation)
    {
        if (projectile != null)
        {
            for (int i = 0; i < _projectileAmmount; i++)
            {
                GameObject instance = Instantiate(projectile, specificSpawnLocation, Quaternion.identity);
                instance.transform.LookAt(playerPosition);
                yield return wfs;
            }
        }

        //Object Pooling
        /*
        else
        {
            for (int i = 0; i < _projectileAmmount; i++)
            {
                GameObject instance = pool.Get();
                instance.transform.LookAt(playerPosition);
                yield return wfs;
            }
        }
        */
    }

    //PatternSpawning
    public void SpawnPattern(float timeInbetween)
    {
        //Debug.Log("Sent");
        StartCoroutine(DefaultPatternSpawner(timeInbetween));
    }

    public void SpawnCustomPattern(float timeInbetween, List<Vector3> vector3s)
    {
        StartCoroutine(CustomPatternSpawner(timeInbetween, vector3s));
    }

    IEnumerator DefaultPatternSpawner(float timeInbetween)
    {
        for (int i = 0; i < projectileSpawnPoints.Count; i++)
        {
            //Debug.Log("Spawnned");
            InitalizeProjectileSpawner(tempProjectile, 1, timeInbetween, projectileSpawnPoints[i].position);
            yield return wfs;
        }
    }

    IEnumerator CustomPatternSpawner(float timeInbetween, List<Vector3> vector3s)
    {
        for (int i = 0; i < vector3s.Count; i++)
        {
            //Debug.Log("Spawnned");
            InitalizeProjectileSpawner(tempProjectile, 1, timeInbetween, vector3s[i]);
            yield return wfs;
        }
    }

    public int SpawnLocationCount
    {
        get { return projectileSpawnPoints.Count; }
    }

    /// <summary>
    /// Stops all coroutines in this projectile spawner
    /// </summary>
    public void StopProjectileSpawner()
    {
        StopAllCoroutines();
    }

}
