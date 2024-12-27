using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterProjectileSpawner : MonoBehaviour, IProjectileSpawner
{
    [Header("Spawner Settings")]
    [SerializeField] private float projectileDamage = 5f;

    [SerializeField] private float projectileSpeed = 30f;

    [SerializeField] private float spawnDelay = 0.2f;

    [SerializeField] private WaitForSeconds waitForSpawnDelay;

    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] List<Transform> projectileSpawnPoints;

    private int index = 0;

    private Vector3 playerPosition => GameManager.Instance.Player.transform.position;

    float IProjectileSpawner.damage => projectileDamage;

    float IProjectileSpawner.speed => projectileSpeed;

    public void Start()
    {
        waitForSpawnDelay = new(spawnDelay);
    }

    void IProjectileSpawner.Spawn(int projectileAmount)
    {
        StartCoroutine(SpawnProjectiles(projectilePrefab, projectileAmount, projectileSpawnPoints)); 
    }

    public IEnumerator SpawnProjectiles(GameObject projectile, int _projectileAmmount, List<Transform> specificSpawnLocation)
    {
        if (projectile != null)
        {
            for (int i = 0; i < _projectileAmmount; i++)
            {
                GameObject instance = Instantiate(projectile, specificSpawnLocation[0].position, Quaternion.identity);
                instance.transform.LookAt(playerPosition);
                yield return null;
            }
        }
    }

    /// <summary>
    /// Stops any more projectiles from spawning
    /// </summary>
    public void StopSpawning() => StopAllCoroutines();
}