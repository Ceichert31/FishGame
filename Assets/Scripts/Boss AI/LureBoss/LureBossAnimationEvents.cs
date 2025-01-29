using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

[System.Serializable]
struct ShockWaveInfo
{
    [SerializeField] GameObject shockWave;
    [SerializeField] Transform shockWaveSpawnPosition;
    [SerializeField] float expandRate;
    [SerializeField] float deathTime;

    public GameObject ShockWave
    {
        get { return shockWave; }
    }

    public Transform ShockWaveSpawnPosition
    {
        get { return shockWaveSpawnPosition; }
    }

    public float ExpandRate
    {
        get { return expandRate; }
    }

    public float DeathTime
    {
        get { return deathTime; }
    }

}

public class LureBossAnimationEvents : AnimationEvents
{
    private LureBossMoveBehavior lureBossMoveBehavior;
    [SerializeField] ShockWaveInfo shockWaveInfo;

    private void Start()
    {
        lureBossMoveBehavior = (LureBossMoveBehavior)bossWalkBehavior;
    }

    IEnumerator ShockThatWave()
    {
        float timer = shockWaveInfo.DeathTime + Time.time;
        Transform shockWave = Instantiate(shockWaveInfo.ShockWave, shockWaveInfo.ShockWaveSpawnPosition.position, Quaternion.identity).transform;
        while(timer > Time.time)
        {
            shockWave.localScale += (Vector3.forward * shockWaveInfo.ExpandRate + Vector3.right * shockWaveInfo.ExpandRate) * Time.deltaTime;
            yield return null;
        }

        Destroy(shockWave.gameObject);
    }

    /// <summary>
    /// 6: Charge Player
    /// 7: Deassign Charge Method and trigger stop charging
    /// 8: Constantly move toward Player
    /// 9: Deassign Constant movment Method
    /// </summary>
    public override void UpdateBossActiveBehavior(int behavior)
    {
        base.UpdateBossActiveBehavior(behavior);

        switch (behavior)
        {
            case 6:
                activeBehavior += lureBossMoveBehavior.ChargePlayer;
                break;
            case 7:
                bossAnimator.SetTrigger("StopCharging");
                activeBehavior -= lureBossMoveBehavior.ChargePlayer;
                break;
            case 8:
                activeBehavior += lureBossMoveBehavior.ConstantMovement;
                break;
            case 9:
                activeBehavior -= lureBossMoveBehavior.ConstantMovement;
                break;
        }
    }
}
