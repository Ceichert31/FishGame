using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    [Header("Hook Settings")]
    [SerializeField] private float parryDelay = 0.8f;

    [SerializeField] private float grappleRange = 20f;

    [SerializeField] private float grappleFireTime = 1f;

    private BobberController bobberController;

    private float parryCooldown;

    private Animator hookAnimator;

    private void Awake()
    {
        hookAnimator = transform.GetChild(1).GetComponent<Animator>();

        bobberController = GetComponentInChildren<BobberController>();
    }

    /// <summary>
    /// Fires raycast to detect if there is a weakpoint
    /// </summary>
    void FireGrapple(InputAction.CallbackContext ctx)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, grappleRange))
        {
            if (hitInfo.collider.CompareTag("Weakpoint"))
            {
                bobberController.StartGrapple(grappleFireTime, hitInfo.point, true);
            }
            else
            {
                bobberController.StartGrapple(grappleFireTime, hitInfo.point, false);
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
