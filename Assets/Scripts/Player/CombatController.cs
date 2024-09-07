using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private AudioPitcherSO spearGunFireAudio;

    [Header("Hook Settings")]
    [SerializeField] private float parryDelay = 0.8f;

    [Header("Harpoon Settings")]
    [SerializeField] private float grappleRange = 20f;

    [Tooltip("Layers that can be fired at")]
    [SerializeField] private LayerMask fireableLayers;

    [Tooltip("Layers that can break the grapple line")]
    [SerializeField] private LayerMask interuptableLayers;

    [Tooltip("How fast the harpoon pulls the player")]
    [SerializeField] private float GrappleForce = 20f;

    [Tooltip("How fast the harpoon retracts and fires")]
    [SerializeField] private float ReelInSpeed = 20f;

    private HarpoonController harpoonController;

    private bool canParry = true;

    private Animator hookAnimator;

    private Animator spearAnimator;

    private AudioSource source;
    private bool IsInProgress => harpoonController.IsInProgress;

    private const int GRAPPLELAYER = 8;

    private const float NULLGRAPPLEDISTANCE = 20f;

    private void Awake()
    {
        hookAnimator = transform.GetChild(1).GetComponent<Animator>();

        spearAnimator = transform.GetChild(3).GetComponent<Animator>();

        harpoonController = GetComponentInChildren<HarpoonController>();

        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Line of Sight, retracts when interupted
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, grappleRange, interuptableLayers))
        {
            harpoonController.Retract(ReelInSpeed);
        }
    }

    /// <summary>
    /// Fires raycast to detect if there is a weakpoint
    /// </summary>
    void FireGrapple(InputAction.CallbackContext ctx)
    {
        if (IsInProgress) return;

        //Play SFX
        spearGunFireAudio.Play(source);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, grappleRange, fireableLayers))
        {
            //If grappable surface is detected, pull player in
            if (hitInfo.collider.gameObject.layer == GRAPPLELAYER)
            {
                //Determine if the grapple point is damageable
                if (hitInfo.collider.CompareTag("Damageable"))
                    harpoonController.StartGrapple(ReelInSpeed, GrappleForce, hitInfo.point, true, true);
                else
                    harpoonController.StartGrapple(ReelInSpeed, GrappleForce, hitInfo.point, true, false);
            }
            else
            {
                harpoonController.StartGrapple(ReelInSpeed, GrappleForce, hitInfo.point, false, false);
            }
        }
        else
        {
            //Unfinished
            //If player fires into space, fire and retract
            Debug.Log(Camera.main.transform.position);

            harpoonController.StartGrapple(ReelInSpeed, GrappleForce, Camera.main.transform.position, false, false);
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
        if (canParry)
        {
            canParry = false;
            
            hookAnimator.SetTrigger("Parry");

            //Delay
            Invoke(nameof(ParryDelay), parryDelay);
        }
    }   

    void ParryDelay() => canParry = true;

    /// <summary>
    /// Used for reseting parry cooldown externally
    /// </summary>
    /// <param name="ctx"></param>
    public void ResetParry(VoidEvent ctx) => canParry = true;

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
