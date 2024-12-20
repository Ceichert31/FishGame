using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassBossAnimationEvents : AnimationEvents
{
    private BassBossMoveBehavoir bassBossMoveBehavior;

    private void Start()
    {
        bassBossMoveBehavior = (BassBossMoveBehavoir)bossWalkBehavior;
    }


    /// <summary>
    /// 4: Charge Player
    /// 5: Deassign Charge Method and trigger stop charging
    /// </summary>
    public override void UpdateBossActiveBehavior(int behavior)
    {
        base.UpdateBossActiveBehavior(behavior);

        switch (behavior)
        {
            case 4:
                //activeBehavior += bassBossMoveBehavior.ChargePlayer;
                break;
            case 5:
                //bossAnimator.SetTrigger("StopCharging");
                //activeBehavior -= bassBossMoveBehavior.ChargePlayer;
                break;
        }
    }
}
