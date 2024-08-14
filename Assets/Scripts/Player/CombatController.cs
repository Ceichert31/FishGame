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
    [SerializeField] private float GrappleForce => GameManager.Instance.PlayerReelInSpeed;

    [Tooltip("How fast the harpoon retracts and fires")]
    [SerializeField] private float ReelInSpeed => GameManager.Instance.PlayerFireRate;

    private HarpoonController harpoonController;

    private float parryCooldown;

    private Animator hookAnimator;

    private Animator spearAnimator;
    private bool IsInProgress => harpoonController.IsInProgress;

    private void Awake()
    {
        hookAnimator = transform.GetChild(1).GetComponent<Animator>();

        spearAnimator = transform.GetChild(3).GetComponent<Animator>();

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
            if (hitInfo.collider.CompareTag("canGrapple"))
            {
                harpoonController.StartGrapple(ReelInSpeed, GrappleForce, hitInfo, true);
            }
            else
            {
                harpoonController.StartGrapple(ReelInSpeed, GrappleForce, hitInfo, false);
            }
        }
    }

    public void Attack()
    {
        spearAnimator.SetTrigger("Attack");
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

    void RetractHook(InputAction.CallbackContext ctx)
    {
        harpoonController.Retract(ReelInSpeed);
    }

    /// <summary>
    /// Subscribes functions to the correct controls
    /// </summary>
    /// <param name="ctx"></param>
    public void InitializeControls(InputEvent ctx)
    {
        ctx.Action.Combat.Disable();

        ctx.Action.Combat.ReelIn.performed += Parry;

        ctx.Action.Combat.ReelIn.performed += RetractHook;

        ctx.Action.Combat.Fire.performed += FireGrapple;
    }
}
