using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FishingAnimatorMethods : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private BoolEventChannel reeledIn_EventChannel;

    private FishingController fishingController;

    private CombatController combatController;

    private Animator animator;

    private BoolEvent trueEvent;

    private BoolEvent falseEvent;

    private void Start()
    {
        fishingController = GetComponentInParent<FishingController>();

        combatController = GetComponentInParent<CombatController>();

        animator = GetComponent<Animator>();

        trueEvent.Value = true;

        falseEvent.Value = false;
    }

    /// <summary>
    /// Called by animator
    /// </summary>
    public void Cast() => fishingController.Cast();

    /// <summary>
    /// Called by animator
    /// </summary>
    public void DisableBobber() => fishingController.DisableBobber();

    /// <summary>
    /// Called by animator
    /// </summary>
    public void CastGrapple() => fishingController.Cast();


    public void ReeledInTrue()
    {
        reeledIn_EventChannel.CallEvent(trueEvent);
    }

    public void ReeledInFalse()
    {
        reeledIn_EventChannel.CallEvent(falseEvent);
    }

    /// <summary>
    /// Sets hooked animation
    /// </summary>
    /// <param name="ctx"></param>
    public void SetHookedAnimation(BoolEvent ctx)
    {
        animator.SetBool("IsFishHooked", ctx.Value);
    }
}
