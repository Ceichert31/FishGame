using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private HookedFishEventChannel hooked_EventChannel;
    [SerializeField] private TextEventChannel text_EventChannel;
    [SerializeField] private BoolEventChannel animation_EventChannel;

    [Header("Spawn Pool Settings")]
    [Tooltip("Fish that can spawn in this water source during the day")]
    [SerializeField] private List<FishSO> daySpawnPool;

    [Tooltip("Fish that can spawn in this water source during the night")]
    [SerializeField] private List<FishSO> nightSpawnPool;

    [Tooltip("How often it will be determined if a fish has been hooked")]
    [SerializeField] private float fishingTickRate = 2f;

    [Tooltip("How long the fish will be hooked before it is lost")]
    [SerializeField] private float fishWaitTime = 3f;

    private bool hasReeledIn;

    private TextEvent noFishEvent;

    private TextEvent fishLostEvent;

    private Coroutine instance = null;

    [SerializeField] private bool isDayTime = true;


    private void Start()
    {
        noFishEvent = new TextEvent("No Fish hooked", 2.5f);

        fishLostEvent = new TextEvent("Fish Got Away...", 2.5f);
    }

    public void StartFishing()
    {
        StopAllCoroutines();

        instance = StartCoroutine(StartFishingTimer());
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

        StartCoroutine(FishHookedWindow());
    }

    IEnumerator FishHookedWindow()
    {
        //Timer
        float currentTime = Time.time + fishWaitTime;

        while (!hasReeledIn)
        {

            //Play animation for fishing rod
            animation_EventChannel.CallEvent(new(true));

            //If player takes too long to reel in exit coroutine
            if (currentTime < Time.time)
            {
                text_EventChannel.CallEvent(fishLostEvent);

                animation_EventChannel.CallEvent(new(false));

                StopAllCoroutines();
            }

            yield return null;
        }

        //If reeled in decide fish
        FishSpawnPool();
    }

    /// <summary>
    /// Returns whether the fish can spawn based on spawn chance
    /// </summary>
    /// <param name="spawnChance"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    bool CalculateSpawnChance(int spawnChance, FishSO instance)
    {
        if (spawnChance <= instance.spawnChance.maxValue && spawnChance > instance.spawnChance.minValue)
            return true;
        else
            return false;
    }

    /// <summary>
    /// When called, Sends a fish Scriptable Object to Boss Manager
    /// </summary>
    void FishSpawnPool()
    {
        //Stop hooked animation
        animation_EventChannel.CallEvent(new(false));

        //Fish spawn chance
        int spawnChance = Random.Range(1, 100);
        
        List<FishSO> spawnableFish = new();

        //Debug.Log("Spawn Chance: " + spawnChance);

        //Day and night time spawn pools
        if (isDayTime)
        {
            //Iterate through each fish and check if fish is within spawn range
            foreach (FishSO instance in daySpawnPool)
            {
                if (CalculateSpawnChance(spawnChance, instance))
                    spawnableFish.Add(instance);
            }
        }
        else
        {
            foreach (FishSO instance in nightSpawnPool)
            {
                if (CalculateSpawnChance(spawnChance, instance))
                    spawnableFish.Add(instance);
            }
        }

        //Randomly select fish from list
        if (spawnableFish.Count > 0) 
        {
            FishSO hookedFish = spawnableFish[Random.Range(0, spawnableFish.Count)];

            //Call event bus to trigger transition
            hooked_EventChannel.CallEvent(new HookedEvent(hookedFish));

            //Transition to arena to fight fish
        }
        else
        {
            //No fish bite
            text_EventChannel.CallEvent(noFishEvent);
        }

    }

    /// <summary>
    /// Listens for the signal the player has reeled in
    /// </summary>
    /// <param name="ctx"></param>
    public void HasReeledIn(BoolEvent ctx)
    {
        hasReeledIn = ctx.Value;

        if (instance != null)
            StopCoroutine(instance);
    }

    public void SetSpawnPoolTime(BoolEvent ctx)
    {
        isDayTime = ctx.Value;
    }
}
