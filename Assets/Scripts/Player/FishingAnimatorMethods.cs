using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingAnimatorMethods : MonoBehaviour
{


    private FishingController fishingController;

    private Animator animator;

    private void Start()
    {
        fishingController = GetComponentInParent<FishingController>();

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

    public void SwitchAnimatorModes(BoolEvent ctx)
    {
        animator.SetBool("IsInCombat", ctx.Value);
    }
}
