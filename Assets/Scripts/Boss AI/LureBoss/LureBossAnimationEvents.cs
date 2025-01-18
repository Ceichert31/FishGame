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
