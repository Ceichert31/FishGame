using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberController : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private VoidEventChannel fov_EventChannel;

    [Header("Bobber References")]
    private LineRenderer bobberLineRenderer;

    private Transform bobberObject;

    private Rigidbody rb;

    private Sequencer sequencer;

    private Transform Player => GameManager.Instance.Player.transform;

    void Start()
    {
        bobberLineRenderer = GetComponent<LineRenderer>();

        bobberObject = transform.GetChild(0);

        rb = bobberObject.GetComponent<Rigidbody>();

        sequencer = GetComponent<Sequencer>();
    }

    void Update()
    {
        bobberLineRenderer.SetPosition(0, transform.position);

        if (bobberObject.gameObject.activeSelf != true) return;

        bobberLineRenderer.SetPosition(1, bobberObject.transform.position);
    }

    /// <summary>
    /// Adds forces to bobber rigidbody
    /// </summary>
    /// <param name="castDistance"></param>
    /// <param name="initialVelocity"></param>
    public void ApplyForcesOnBobber(float castDistance, float initialVelocity)
    {
        DisableBobber();

        EnableBobber(false);

        //Apply forces
        rb.velocity = castDistance * new Vector3(transform.parent.forward.x, initialVelocity, transform.parent.forward.z);
    }

    /// <summary>
    /// Enabled the bobber gameobject
    /// </summary>
    /// <param name="isKinematic"></param>
    public void EnableBobber(bool isKinematic)
    {
        //Enable bobber
        bobberObject.gameObject.SetActive(true);

        //Set kinematic
        rb.isKinematic = isKinematic;

        //Clear parent
        bobberObject.transform.parent = null;

        //Enable line renderer
        bobberLineRenderer.enabled = true;
    }

    /// <summary>
    /// Disables and reset the bobber gameobject
    /// </summary>
    public void DisableBobber()
    {
        //Reset line renderer position
        bobberLineRenderer.SetPosition(1, transform.position);

        //Disable line renderer
        bobberLineRenderer.enabled = false;

        //Reset parent
        bobberObject.transform.parent = transform;

        //Reset position
        bobberObject.transform.position = transform.position;

        //Disable gameobject
        bobberObject.gameObject.SetActive(false);
    }

    /// <summary>
    /// Starts grapple coroutine
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="hitPoint"></param>
    /// <param name="isWeakPoint"></param>
    public void StartGrapple(float totalTime, Vector3 hitPoint, bool isWeakPoint) => StartCoroutine(ShootGrapple(totalTime, hitPoint, isWeakPoint));

    /// <summary>
    /// Lerps bobber to target position
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="hitPoint"></param>
    /// <param name="isWeakPoint"></param>
    /// <returns></returns>
    IEnumerator ShootGrapple(float totalTime, Vector3 hitPoint, bool isWeakPoint)
    {
        EnableBobber(true);

        Vector3 startPos = bobberObject.position;

        float timeElapsed = 0f;

        while (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;

            bobberObject.transform.position = Vector3.Lerp(startPos, hitPoint, timeElapsed / totalTime);

            yield return null;
        }

        bobberObject.position = hitPoint;

        if (isWeakPoint)
        {
            StartCoroutine(GrapplePlayer(totalTime, hitPoint));
        }
        else
        {
            StartCoroutine(RetractGrapple(totalTime));
        }
    }
    
    /// <summary>
    /// Lerps player to target position
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="hitPoint"></param>
    /// <returns></returns>
    IEnumerator GrapplePlayer(float totalTime, Vector3 hitPoint)
    {
        Vector3 startPos = Player.position;

        float timeElapsed = 0f;

        fov_EventChannel.CallEvent(new());

        sequencer.InitializeSequence();

        while (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;

            Player.position = Vector3.Lerp(startPos, hitPoint, timeElapsed / totalTime);

            yield return null;
        }

        DisableBobber();
    }

    /// <summary>
    /// Lerps bobber back to starting position
    /// </summary>
    /// <param name="totalTime"></param>
    /// <returns></returns>
    IEnumerator RetractGrapple(float totalTime)
    {
        Vector3 startPos = bobberObject.position;

        float timeElapsed = 0f;

        while (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;

            bobberObject.position = Vector3.Lerp(startPos, transform.position, timeElapsed / totalTime);

            yield return null;
        }

        DisableBobber();
    }

    /// <summary>
    /// Called by transition manager
    /// </summary>
    /// <param name="ctx"></param>
    public void ResetBobber(VoidEvent ctx) => DisableBobber();
}
