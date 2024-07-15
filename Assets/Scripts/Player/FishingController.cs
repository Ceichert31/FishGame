using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class FishingController : MonoBehaviour
{   
    [Header("Fishing Pole Settings")]
    [Tooltip("The maximum amount of charge the pole can gain")]
    [SerializeField] private float maxPoleCharge = 10f;

    [Tooltip("The initial Y-Velocity of the bobber")]
    [SerializeField] private float initialBobberVelocityY = 1.3f;

    [Tooltip("How fast the pole will charge")]
    [SerializeField] private float chargeTimeMultiplier = 1.5f;

    private BobberController bobberController;

    private Animator poleAnimator;

    private bool isCharging;

    private float currentPoleCharge;

    private const float MINPOLECHARGE = 2f;

    void Awake()
    {
        poleAnimator = GetComponent<Animator>();

        bobberController = GetComponentInChildren<BobberController>();
    }

    void ChargeCast(bool isCast)
    {
        if (!isCast)
            StartCoroutine(ChargeFishingPole());
        else
            isCharging = false;
    }

    IEnumerator ChargeFishingPole()
    {
        currentPoleCharge = MINPOLECHARGE;

        isCharging = true;

        poleAnimator.SetBool("IsCharging", isCharging);

        while (isCharging)
        {
            currentPoleCharge += Time.unscaledDeltaTime * chargeTimeMultiplier;

            Mathf.Clamp(currentPoleCharge, MINPOLECHARGE, maxPoleCharge);


            if (currentPoleCharge >= maxPoleCharge)
            {
                Debug.Log("Fully Charged!");
            }

            yield return null;
        }

        poleAnimator.SetBool("IsCharging", isCharging);
    }

    /// <summary>
    /// Called by animator
    /// </summary>
    public void Cast()
    {
        bobberController.ApplyForcesOnBobber(currentPoleCharge, initialBobberVelocityY);
    }

    /// <summary>
    /// Called by animator
    /// </summary>
    public void DisableBobber() => bobberController.DisableBobber();

    /// <summary>
    /// Changes animation to reel in animation
    /// </summary>
    /// <param name="ctx"></param>
    void ReelIn(InputAction.CallbackContext ctx) => poleAnimator.SetTrigger("ReelIn");

    public void StartCast(InputAction.CallbackContext ctx) => ChargeCast(false);

    public void StopCast(InputAction.CallbackContext ctx) => ChargeCast(true);

    /// <summary>
    /// Subscribes functions to the correct controls
    /// </summary>
    /// <param name="ctx"></param>
    public void InitializeControls(InputEvent ctx)
    {
        ctx.Action.Movement.ReelIn.performed += ReelIn;

        ctx.Action.Movement.Fire.performed += StartCast;

        ctx.Action.Movement.Fire.canceled += StopCast;
    }
}
