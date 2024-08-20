using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonController : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private VoidEventChannel fov_EventChannel;
    [SerializeField] private FloatEventChannel damage_EventChannel;

    private CombatController combatController;

    private Sequencer attackSequencer;

    private LineRenderer boltLineRenderer;

    private Transform boltObject;

    private Rigidbody boltRigidbody;
    private Transform Player => GameManager.Instance.Player.transform;
    private Rigidbody playerRigidbody => GameManager.Instance.Player.GetComponent<Rigidbody>();

    private bool isInProgress;

    //Accessors
    public bool IsInProgress { get { return isInProgress; } }

    //Constants
    private Vector3 HOOKRESETPOSITION = new(40f, 0, 0);

    private const float GRAPPLEDISTANCE = 1f;

    private void Start()
    {
        combatController = GetComponentInParent<CombatController>();

        boltLineRenderer = GetComponent<LineRenderer>();

        attackSequencer = GetComponent<Sequencer>();

        boltObject = transform.GetChild(0);

        boltRigidbody = boltObject.GetComponent<Rigidbody>();

        //Disabled after game starts
        transform.parent.gameObject.SetActive(false);
    }

    void Update()
    {
        boltLineRenderer.SetPosition(0, transform.position);

        if (boltObject.gameObject.activeSelf != true) return;

        boltLineRenderer.SetPosition(1, boltObject.transform.position);
    }

    /// <summary>
    /// Starts grapple coroutine
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="hitPoint"></param>
    /// <param name="isWeakPoint"></param>
    public void StartGrapple(float reelInSpeed, float grappleForce, RaycastHit hitPoint, bool isWeakPoint)
    {
        //Disable firing harpoon
        isInProgress = true;

        StartCoroutine(ShootGrapple(reelInSpeed, grappleForce, hitPoint, isWeakPoint));
    }
    /// <summary>
    /// Lerps bobber to target position
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="hitPoint"></param>
    /// <param name="isWeakPoint"></param>
    /// <returns></returns>
    IEnumerator ShootGrapple(float reelInSpeed, float grappleForce, RaycastHit hitPoint, bool isWeakPoint)
    {
        boltRigidbody.isKinematic = false;

        //Unparent bolt
        boltObject.parent = null;

        //Move bolt to hitpoint
        while (Vector3.Distance(hitPoint.point, boltObject.position) > GRAPPLEDISTANCE)
        {
            //boltObject.transform.position = Vector3.MoveTowards(boltObject.transform.position, hitPoint.point, reelInSpeed * Time.deltaTime);

            boltRigidbody.velocity = reelInSpeed * (hitPoint.point - boltObject.position).normalized;

            yield return null;
        }

        //Stop movement
        boltRigidbody.velocity = Vector3.zero;

        //Set final position
        boltObject.position = hitPoint.point;

        //Determine if player can grapple to surface or not
        if (isWeakPoint)
        {
            StartCoroutine(GrapplePlayer(grappleForce, hitPoint));
        }
        else
        {
            StartCoroutine(RetractGrapple(reelInSpeed));
        }
    }

    /// <summary>
    /// Lerps player to target position
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="hitPoint"></param>
    /// <returns></returns>
    IEnumerator GrapplePlayer(float grappleForce, RaycastHit hitPoint)
    {
        FloatEvent distance;

        distance.FloatValue = Vector3.Distance(Player.position, hitPoint.point);

        //Increase FOV
        fov_EventChannel.CallEvent(new());

        //Move player to hitpoint
        while (Vector3.Distance(hitPoint.point, Player.position) > GRAPPLEDISTANCE)
        {
            playerRigidbody.velocity = grappleForce * (hitPoint.point - Player.position).normalized;

            yield return null;
        }

        //If weakpoint, deal damage
        if (hitPoint.transform.gameObject.CompareTag("Damageable"))
        {
            //Stop velocity
            playerRigidbody.velocity = Vector3.zero;

            //Play attack effects
            attackSequencer.InitializeSequence();

            //Play attack animation
            combatController.Attack();

            //Deal damage
            damage_EventChannel.CallEvent(new(GameManager.Instance.PlayerDamage));
        }

        ResetBolt();
    }

    /// <summary>
    /// Lerps bobber back to starting position
    /// </summary>
    /// <param name="totalTime"></param>
    /// <returns></returns>
    IEnumerator RetractGrapple(float reelInSpeed)
    {
        while (Vector3.Distance(transform.position, boltObject.position) > GRAPPLEDISTANCE)
        {
            //boltObject.transform.position = Vector3.MoveTowards(boltObject.transform.position, transform.position, reelInSpeed * Time.deltaTime);

            boltRigidbody.velocity = reelInSpeed * (transform.position - boltObject.position).normalized;

            //If bolt gets too far away, reset it
            if (Vector3.Distance(transform.position, boltObject.position) > 30f)
            {
                ResetBolt();
            }

            yield return null;
        }

        //Stop movement
        boltRigidbody.velocity = Vector3.zero;

        ResetBolt();
    }

    /// <summary>
    /// Starts retracting the harpoon bolt
    /// </summary>
    /// <param name="reelInSpeed"></param>
    public void Retract(float reelInSpeed)
    {
        StopAllCoroutines();

        StartCoroutine(RetractGrapple(reelInSpeed));
    }

    void ResetBolt()
    {
        StopAllCoroutines();

        //Set rigidbody to kinematic
        boltRigidbody.isKinematic = true;

        //Clear players parent
        Player.parent = null;

        //Reset parent
        boltObject.parent = transform;

        //Reset position
        boltObject.localPosition = Vector3.zero;

        //Reset rotation
        boltObject.localEulerAngles = HOOKRESETPOSITION;

        //Re-enable firing harpoon
        isInProgress = false;
    }
}

