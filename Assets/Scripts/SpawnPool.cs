using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{

    [Header("Spawn Pool Settings")]
    [Tooltip("Fish that can spawn in this water source")]
    [SerializeField] private List<FishSO> spawnPool;

    [ContextMenu("Spawn Fish")]
    void FishSpawnPool()
    {
        int spawnChance = Random.Range(1, 101);
        Debug.Log("Spawn Chance: " + spawnChance);
        List<FishSO> spawnableFish = new();

        foreach (FishSO instance in spawnPool) 
        {
            if (spawnChance <= instance.spawnChance.maxValue && spawnChance > instance.spawnChance.minValue)
                spawnableFish.Add(instance);
        }

        if (spawnableFish.Count > 0) 
        {
            FishSO hookedFish = spawnableFish[Random.Range(0, spawnableFish.Count)];

            Debug.Log(hookedFish.name);

            //Transition to arena to fight fish
        }
        else
        {
            Debug.Log("No Fish Bite");
            //No fish bite
        }

    }
}
