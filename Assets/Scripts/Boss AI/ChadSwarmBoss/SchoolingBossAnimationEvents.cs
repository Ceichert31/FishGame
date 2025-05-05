using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

public class SchoolingBossAnimationEvents : AnimationEvents
{
    [SerializeField] SchoolingBossMovementBehavior schoolingBossMoveBehavior;

    private Animator schoolingAnimator;
    private float animationSpeed;

    [SerializeField] Flock flock;
    [SerializeField] FlockStateController flockStateController;

    private void Start()
    {

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

        Debug.Log(behavior);

        switch (behavior)
        {
            case 6:
                activeBehavior += schoolingBossMoveBehavior.ChargePlayer;
                break;
            case 7:
                bossAnimator.SetTrigger("StopCharging");
                activeBehavior -= schoolingBossMoveBehavior.ChargePlayer;
                break;
            case 8:
                activeBehavior += schoolingBossMoveBehavior.ConstantMovement;
                break;
            case 9:
                activeBehavior -= schoolingBossMoveBehavior.ConstantMovement;
                break;
            default:
                break;
        }
    }

    public void StartFlocking()
    {
        flock.ShouldFlock = true;
        flockStateController.GoThere = false;
    }

    public void MorphToShape()
    {
        flock.ShouldFlock = false;
        flockStateController.GoThere = true;
    }
}
