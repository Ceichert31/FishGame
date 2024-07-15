using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{   
    [Header("Fishing Pole Settings")]
    [Tooltip("The maximum amount of charge the pole can gain")]
    [SerializeField] private float maxPoleCharge = 10f;

    [Tooltip("The initial Y-Velocity of the bobber")]
    [SerializeField] private float initialBobberVelocityY = 1.3f;

    [Tooltip("How fast the pole will charge")]
    [SerializeField] private float chargeTimeMultiplier = 1.5f;

    private InputController inputController;

    private BobberController bobberController;

    private Animator poleAnimator;

    private bool isCharging;

    private float currentPoleCharge;

    private const float MINPOLECHARGE = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        inputController = GetComponentInParent<InputController>();

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

            if (currentPoleCharge >= maxPoleCharge)
                isCharging = false;

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

    /// <summary>
    /// Subscribes functions to the correct controls
    /// </summary>
    /// <param name="ctx"></param>
    public void InitializeControls(InputEvent ctx)
    {
        ctx.Action.Movement.ReelIn.performed += ReelIn;
    }
    private void OnEnable()
    {
        inputController.castPole += ChargeCast;
    }
    private void OnDisable()
    {
        inputController.castPole -= ChargeCast;
    }
}
