using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingController : MonoBehaviour
{   
    [Header("Fishing Pole Settings")]
    [Tooltip("The maximum distance the bobber can be cast")]
    [SerializeField] private float maxPoleCharge = 10f;

    [Tooltip("How fast the pole will charge")]
    [SerializeField] private float chargeTimeMultiplier = 1.5f;

    [SerializeField] private float chargeMaxThreshold = 5f;

    private GameObject bobberObject;

    private Rigidbody rb;

    private InputController inputController;

    private Animator poleAnimator;

    private bool isCharging;

    // Start is called before the first frame update
    void Awake()
    {
        inputController = GetComponentInParent<InputController>();

        poleAnimator = GetComponent<Animator>();

        bobberObject = transform.GetChild(1).GetChild(0).gameObject;

        rb = bobberObject.GetComponent<Rigidbody>();
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
        float currentPoleCharge = 1f;

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

        CastPole(currentPoleCharge);
    }

    void CastPole(float castDistance)
    {
        bobberObject.SetActive(true);

        rb.AddForce(castDistance * transform.up, ForceMode.Impulse);
        rb.AddForce(castDistance * transform.forward, ForceMode.Force);
        
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
