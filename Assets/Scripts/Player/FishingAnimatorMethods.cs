using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingAnimatorMethods : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private VoidEventChannel reeledIn_EventChannel;

    private FishingController fishingController;

    private CombatController combatController;

    private Animator animator;

    private void Start()
    {
        fishingController = GetComponentInParent<FishingController>();

        combatController = GetComponentInParent<CombatController>();

        animator = GetComponent<Animator>();
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


    public void ReeledInEventCall()
    {
        reeledIn_EventChannel.CallEvent(new());
    }
}
