using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private AudioPitcherSO castingAudio;
    [SerializeField] private AudioPitcherSO reelInAudio;

    [Header("Fishing Pole Settings")]
    [Tooltip("The maximum amount of charge the pole can gain")]
    [SerializeField] private float maxPoleCharge = 10f;

    [Tooltip("The initial Y-Velocity of the bobber")]
    [SerializeField] private float initialBobberVelocityY = 1.3f;

    [Tooltip("How fast the pole will charge")]
    [SerializeField] private float chargeTimeMultiplier = 1.5f;

    [SerializeField] private BobberController bobberController;

    private Animator poleAnimator;

    private bool isCharging;

    private bool alreadyCast;

    private float currentPoleCharge;

    private GameObject fishingRod;

    private AudioSource source;

    private const float MINPOLECHARGE = 2f;

    void Start()
    {
        fishingRod = transform.GetChild(0).gameObject;

        poleAnimator = fishingRod.GetComponent<Animator>();

        bobberController = GetComponentInChildren<BobberController>();

        source = GetComponent<AudioSource>();

        if (GameManager.Instance.firstTimeLoading)
        {
            transform.GetChild(0).gameObject.SetActive(false);

            GameManager.Instance.firstTimeLoading = false;
        }
    }

    /// <summary>
    /// Initiate casting fishing rod
    /// </summary>
    /// <param name="isCast"></param>
    void ChargeCast(bool isCast)
    {
        if (alreadyCast) return;

        if (!isCast)
            StartCoroutine(ChargeFishingPole());
        else
            isCharging = false;
    }

    /// <summary>
    /// Determine amount of power when casting
    /// </summary>
    /// <returns></returns>
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
    /// Apply forces to bobber
    /// </summary>
    public void Cast()
    {
        alreadyCast = true;

        castingAudio.Play(source);

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
    void ReelIn(InputAction.CallbackContext ctx)
    {
        if (fishingRod.activeSelf != true) return;

        alreadyCast = false;

        reelInAudio.Play(source);

        //Trigger animation
        poleAnimator.SetTrigger("ReelIn");
    }

    public void StartCast(InputAction.CallbackContext ctx) => ChargeCast(false);

    public void StopCast(InputAction.CallbackContext ctx) => ChargeCast(true);

    /// <summary>
    /// Subscribes functions to the correct controls
    /// </summary>
    /// <param name="ctx"></param>
    public void InitializeControls(InputEvent ctx)
    {
        ctx.Action.Fishing.Enable();

        ctx.Action.Fishing.ReelIn.performed += ReelIn;

        ctx.Action.Fishing.Fire.performed += StartCast;

        ctx.Action.Fishing.Fire.canceled += StopCast;
    }
}
