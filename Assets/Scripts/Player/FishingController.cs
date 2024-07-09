using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InputController))]
public class FishingController : MonoBehaviour
{   
    [Header("Fishing Pole Settings")]
    [Tooltip("The maximum distance the bobber can be cast")]
    [SerializeField] private float maxPoleCharge = 10f;

    [Tooltip("How fast the pole will charge")]
    [SerializeField] private float chargeTimeMultiplier = 1.5f;

    private InputController inputController;

    private bool isCharging;

    // Start is called before the first frame update
    void Awake()
    {
        inputController = GetComponent<InputController>();
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

        while (isCharging)
        {
            currentPoleCharge += Time.unscaledDeltaTime * chargeTimeMultiplier;

            if (currentPoleCharge >= maxPoleCharge)
                isCharging = false;

            Debug.Log("Current Charge: " +  currentPoleCharge);

            yield return null;
        }
        Debug.Log("CAST!");
        CastPole(currentPoleCharge);
    }

    void CastPole(float castDistance)
    {
        Debug.Log(castDistance);

        //Cast bobber with addforce
        //Bobber needs rigidbody
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
