using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    [Header("Hook Settings")]
    [SerializeField] private float parryDelay = 0.8f;

    [Header("Harpoon Settings")]
    [SerializeField] private float grappleRange = 20f;

    [Tooltip("How fast the harpoon pulls the player")]
    [SerializeField] private float grappleForce = 5f;

    [Tooltip("How fast the harpoon retracts and fires")]
    [SerializeField] private float reelInSpeed = 10f;

    private HarpoonController harpoonController;

    private float parryCooldown;

    private Animator hookAnimator;
    private bool IsInProgress => harpoonController.IsInProgress;

    private void Awake()
    {
        hookAnimator = transform.GetChild(1).GetComponent<Animator>();

        harpoonController = GetComponentInChildren<HarpoonController>();
    }

    /// <summary>
    /// Fires raycast to detect if there is a weakpoint
    /// </summary>
    void FireGrapple(InputAction.CallbackContext ctx)
    {
        if (IsInProgress) return;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, grappleRange))
        {
            if (hitInfo.collider.CompareTag("Weakpoint"))
            {
                harpoonController.StartGrapple(reelInSpeed, grappleForce, hitInfo.point, true);
            }
            else
            {
                harpoonController.StartGrapple(reelInSpeed, grappleForce, hitInfo.point, false);
            }
        }
    }

    /// <summary>
    /// Plays the parry animation
    /// </summary>
    /// <param name="ctx"></param>
    void Parry(InputAction.CallbackContext ctx) 
    {
        //Time gate to prevent queued parries
        if (parryCooldown <= Time.time)
        {
            parryCooldown = Time.time + parryDelay;

            hookAnimator.SetTrigger("Parry");
        }
    }   

    /// <summary>
    /// Subscribes functions to the correct controls
    /// </summary>
    /// <param name="ctx"></param>
    public void InitializeControls(InputEvent ctx)
    {
        ctx.Action.Combat.Disable();

        ctx.Action.Combat.ReelIn.performed += Parry;

        ctx.Action.Combat.Fire.performed += FireGrapple;
    }
}
