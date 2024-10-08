using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GrappleSurface
{
    normal,
    damageable,
    weakPoint,
}
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

    private HarpoonController harpoonController;

    private bool canParry = true;

    private Animator hookAnimator;

    private Animator spearAnimator;

    private AudioSource source;
    private bool inProgress => harpoonController.InProgress;
    private bool TimerIsUp => harpoonController.TimerIsUp;

    private bool canFire;

    private const int GRAPPLELAYER = 8;

    private const float NULLGRAPPLEDISTANCE = 20f;

    private void Awake()
    {
        hookAnimator = transform.GetChild(1).GetComponent<Animator>();

        spearAnimator = transform.GetChild(3).GetComponent<Animator>();

        harpoonController = GetComponentInChildren<HarpoonController>();

        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Fires raycast to detect if there is a weakpoint
    /// </summary>
    void FireGrapple(InputAction.CallbackContext ctx)
    {
        canFire = inProgress ? false : TimerIsUp;

        if (!canFire) return;

        //Play SFX
        spearGunFireAudio.Play(source);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, grappleRange, fireableLayers))
        {
            //If grappable surface is detected, pull player in
            if (hitInfo.collider.gameObject.layer == GRAPPLELAYER)
            {
                //Determine if the grapple point is damageable
                if (hitInfo.collider.CompareTag("Damageable"))
                    harpoonController.StartGrapple(hitInfo.point, true, GrappleSurface.damageable);
                else if (hitInfo.collider.CompareTag("Weakpoint"))
                    harpoonController.StartGrapple(hitInfo.point, true, GrappleSurface.weakPoint);
            }
            else
            {
                harpoonController.StartGrapple(hitInfo.point, false, GrappleSurface.normal);
            }
        }
        else
        {
            //Unfinished
            //If player fires into space, fire and retract
            Debug.Log(Camera.main.transform.position);

            harpoonController.StartGrapple(Camera.main.transform.position, false, GrappleSurface.normal);
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
        if(inProgress)
        {
            return;
        }

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
        harpoonController.Retract();
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
