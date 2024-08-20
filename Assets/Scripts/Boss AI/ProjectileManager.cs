using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ProjectileManager : MonoBehaviour
{
    //Object Pool
    protected ObjectPool<GameObject> pool;
    WaitForSeconds lifeTime = new WaitForSeconds(10);

    [Header("Object Pool Settings")]
    //What object will be pool along with, how many objects we allow in the pool at once, the max size of the pool
    [SerializeField] protected GameObject pooledObject;
    [SerializeField] protected int poolSize;
    [SerializeField] protected int maxPoolSize;

    //All variables that are here to be transfered from method to coroutine
    [Header("TempProjectileInfo")]
    protected Transform spefSpawnLocation;
    protected WaitForSeconds wfs;

    /// <summary>
    /// Initializes the pool
    /// </summary>
    public virtual void OnStart()
    {
        pool = new ObjectPool<GameObject>(CreateObject, EnableObject, DisableObject, DestroyOverflow, false, poolSize, maxPoolSize);
    }

    /// <summary>
    /// Must be called in order to spawn objects, if you want normal proj make first variable null
    /// </summary>
    /// <param name="_projectileAmmount"></param>
    /// <param name="timeBetweenProjectile"></param>
    /// <param name="specificSpawnLocation"></param>
    public virtual void InitalizeProjectileSpawner(GameObject projectile, int _projectileAmmount, float timeBetweenProjectile, Vector3 specificSpawnLocation)
    {
        wfs = new WaitForSeconds(timeBetweenProjectile);
        StartCoroutine(SpawnProjectiles(projectile, _projectileAmmount, specificSpawnLocation));
    }

    /// <summary>
    /// IEnumerator that spawns objects based on subscribed behavior
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator SpawnProjectiles(GameObject projectile, int _projectileAmmount, Vector3 specificSpawnLocation);

    //All methods below are for the creation of the object pool, they are virtual so their behavior is completely up to you if u want to modify it

    public virtual GameObject CreateObject()
    {
        return Instantiate(pooledObject, spefSpawnLocation.position, Quaternion.identity);
    }
    
    public virtual void EnableObject(GameObject obj)
    {
        obj.transform.position = spefSpawnLocation.position;
        obj.SetActive(true);
        StartCoroutine(ReleaseProjectile(obj));
    }
    public virtual void DisableObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    public virtual void DestroyOverflow(GameObject obj)
    {
        Destroy(obj);
    }

    IEnumerator ReleaseProjectile(GameObject obj)
    {
        yield return lifeTime;
        if(obj != null)
        {
            pool.Release(obj);
        }
    }
}
