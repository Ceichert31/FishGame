using System.Collections;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    [Header("Field of View Settings")]
    [SerializeField] private float targetFOV = 10f;
    [SerializeField] private float decayTime = 1f;

    [SerializeField] private float fieldOfViewScale = 10f;
    [SerializeField] private float baseFOV = 60f;
    [SerializeField] private float maxFOV = 100f;
    [SerializeField] private float smoothTime = 0.2f;

    private Camera mainCamera;

    private Rigidbody rb;

    private float fieldOfViewTarget;

    private float additionalFOV;

    private float currentVelocity;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCamera = GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        float scaledVelocity = rb.velocity.magnitude / fieldOfViewScale;

        fieldOfViewTarget = baseFOV + scaledVelocity;

        Mathf.Clamp(fieldOfViewTarget, baseFOV, maxFOV);

        mainCamera.fieldOfView = Mathf.SmoothDamp(mainCamera.fieldOfView, fieldOfViewTarget + additionalFOV, ref currentVelocity, smoothTime);
    }

    /// <summary>
    /// Inreases cameras FOV when called
    /// </summary>
    /// <param name="targetFOV"></param>
    /// <param name="timeBeforeDecay"></param>
    public void IncreaseFOV(VoidEvent ctx)
    {
        additionalFOV = targetFOV;
        StartCoroutine(DecreaseFOV(targetFOV, decayTime));
    }
    IEnumerator DecreaseFOV(float targetFOV, float timeBeforeDecay)
    {
        yield return new WaitForSeconds(timeBeforeDecay);
        additionalFOV -= targetFOV;
    }
}
