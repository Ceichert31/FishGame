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

    void Start()
    {
        bobberLineRenderer = GetComponent<LineRenderer>();

        bobberObject = transform.GetChild(0);

        rb = bobberObject.GetComponent<Rigidbody>();
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

        EnableBobber();

        //Apply forces
        rb.velocity = castDistance * new Vector3(transform.parent.forward.x, initialVelocity, transform.parent.forward.z);
    }

    /// <summary>
    /// Enabled the bobber gameobject
    /// </summary>
    /// <param name="isKinematic"></param>
    public void EnableBobber()
    {
        //Enable bobber
        bobberObject.gameObject.SetActive(true);

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
    /// Called by transition manager
    /// </summary>
    /// <param name="ctx"></param>
    public void ResetBobber(VoidEvent ctx) => DisableBobber();
}
