using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private SequencerActionWaitForCutscene cutsceneWaitTime;

    [SerializeField] private Transform bossSpawner;

    private HookedEvent cachedBoss;

    /// <summary>
    /// Caches the fish caught before spawning it
    /// </summary>
    /// <param name="ctx"></param>
    public void CacheBoss(HookedEvent ctx)
    {
        cachedBoss = ctx;

        //Get the length of the bosses cutscene, if the boss doesn't have a cutscene, set length to zero
        if (cachedBoss.fishInstance.fishPrefab.TryGetComponent(out PlayableDirector director))
            cutsceneWaitTime.cutsceneLength = (float)director.duration;
        else
            cutsceneWaitTime.cutsceneLength = 0;
    }

    /// <summary>
    /// Instantiates the cached boss
    /// </summary>
    /// <param name="ctx"></param>
    public void SpawnCachedBoss(VoidEvent ctx)
    {
        Instantiate(cachedBoss.fishInstance.fishPrefab, bossSpawner.position, Quaternion.identity);
    }
}
