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

[System.Serializable]
public enum TutorialState
{
    Parry,
    Fire,
    Reload,
    Unparryable,
}

public class LureBossAnimationEvents : AnimationEvents
{
    private LureBossMoveBehavior lureBossMoveBehavior;
    [SerializeField] ShockWaveInfo shockWaveInfo;

    [Header("Tutorial References")]
    [SerializeField] private TextEventChannel tutorialTextEventChannel;
    [SerializeField] private ComponentEventChannel playerComponentChannel;

    private ComponentEvent disablePlayerComp;
    private ComponentEvent enablePlayerComp;

    private TextEvent parryTutorial;
    private TextEvent fireTutorial;
    private TextEvent reloadTutorial;
    private TextEvent unparryableTutorial;

    private Animator lureAnimator;
    private float animationSpeed;

    private void Start()
    {
        lureBossMoveBehavior = (LureBossMoveBehavior)bossWalkBehavior;

        //Init tutorial text
        parryTutorial = new TextEvent("Press [RMB] to parry", 0);
        fireTutorial = new TextEvent("Press [LMB] to fire", 0);
        reloadTutorial = new TextEvent("Press [R] to reload", 0);
        unparryableTutorial = new TextEvent("Press [Shift + S] to dash backwards", 0);

        disablePlayerComp = new ComponentEvent(true, true, false, false);
        enablePlayerComp = new ComponentEvent(true, true, true, true);

        lureAnimator = GetComponent<Animator>();
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

    /// <summary>
    /// Sets animator bool to false after playing tutorial
    /// </summary>
    public void HasPlayedTutorial()
    {
        lureAnimator.SetBool("PlayTutorial", false);
    }

    public void TutorialBehavior(TutorialState state)
    {
        switch (state)
        {
            case TutorialState.Parry:
                tutorialTextEventChannel.CallEvent(parryTutorial);
                break;

            case TutorialState.Fire:
                tutorialTextEventChannel.CallEvent(fireTutorial);
                break;

            case TutorialState.Reload:
                tutorialTextEventChannel.CallEvent(reloadTutorial);
                break;

            case TutorialState.Unparryable:
                tutorialTextEventChannel.CallEvent(unparryableTutorial);
                break;
        }

        //Freeze boss/pause anim time
        animationSpeed = lureAnimator.speed;
        lureAnimator.speed = 0;
    }

    public void ClearTutorial(VoidEvent ctx)
    {
        lureAnimator.speed = animationSpeed;
    }

    public void EnablePlayerComponents()
    {
        playerComponentChannel.CallEvent(enablePlayerComp);
    }
    public void DisablePlayerComponents()
    {
        playerComponentChannel.CallEvent(disablePlayerComp);
    }
}
