using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private Transform bossSpawner;

    public void SpawnBoss(HookedEvent ctx)
    {
        Instantiate(ctx.fishInstance.fishPrefab, bossSpawner.position, Quaternion.identity);
    }
}