using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private HookedFishEventChannel hooked_EventChannel;

    [Header("Spawn Pool Settings")]
    [Tooltip("Fish that can spawn in this water source")]
    [SerializeField] private List<FishSO> spawnPool;

    [Tooltip("How often it will be determined if a fish has been hooked")]
    [SerializeField] private float fishingTickRate = 2f;

    public void StartFishing()
    {
        StartCoroutine(StartFishingTimer());
    }

    IEnumerator StartFishingTimer()
    {
        bool caughtFish = false;

        while (!caughtFish)
        {
            //Wait for tick
            yield return new WaitForSeconds(fishingTickRate);

            //Decide whether a fish will be hooked on this tick
            int chance = Random.Range(0, 1);

            if (chance == 0)
                caughtFish = true;
                
            yield return null;
        }

        FishSpawnPool();
    }

    void FishSpawnPool()
    {
        //Fish spawn chance
        int spawnChance = Random.Range(1, 101);
        
        List<FishSO> spawnableFish = new();

        //Iterate through each fish and check if fish is within spawn range
        foreach (FishSO instance in spawnPool) 
        {
            if (spawnChance <= instance.spawnChance.maxValue && spawnChance > instance.spawnChance.minValue)
                spawnableFish.Add(instance);
        }

        //Randomly select fish from list
        if (spawnableFish.Count > 0) 
        {
            FishSO hookedFish = spawnableFish[Random.Range(0, spawnableFish.Count)];

            //Debug.Log("Spawn Chance: " + spawnChance);
            //Debug.Log(hookedFish.name);

            //Call event bus to trigger transition
            hooked_EventChannel.CallEvent(new HookedEvent(hookedFish));

            //Transition to arena to fight fish
        }
        else
        {
            Debug.Log("No Fish Bite");
            //No fish bite
        }

    }
}
