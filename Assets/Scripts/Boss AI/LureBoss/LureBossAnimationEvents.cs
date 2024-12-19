using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

public class LureBossAnimationEvents : AnimationEvents
{
    private LureBossMoveBehavior lureBossMoveBehavior;

    private void Start()
    {
        lureBossMoveBehavior = (LureBossMoveBehavior)bossWalkBehavior;
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
                activeBehavior += lureBossMoveBehavior.ChargePlayer;
                break;
            case 5:
                bossAnimator.SetTrigger("StopCharging");
                activeBehavior -= lureBossMoveBehavior.ChargePlayer;
                break;
        }
    }
}
