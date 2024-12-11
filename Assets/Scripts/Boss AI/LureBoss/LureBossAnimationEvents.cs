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
    /// 5: Deassign Charge Method
    /// </summary>
    /// <param name="behavior"></param>
    public void AditionalBossActiveBehavior(int behavior)
    {
        switch (behavior)
        {
            case 4:
                activeBehavior += lureBossMoveBehavior.ChargePlayer;
                break;
            case 5:
                activeBehavior -= lureBossMoveBehavior.ChargePlayer;
                break;
        }
    }
}
