using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;
using Unity.VisualScripting;

public class HarpoonController : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private VoidEventChannel fov_EventChannel;
    [SerializeField] private FloatEventChannel damage_EventChannel;
    [SerializeField] private VoidEventChannel weakPoint_EventChannel;

    [Tooltip("How fast the harpoon pulls the player")]
    [SerializeField] private float grappleForce = 20f;

    [Tooltip("How fast the harpoon retracts and fires")]
    [SerializeField] private float reelInSpeed = 20f;

    [Tooltip("How long until the player can fire the grappling g")]
    [SerializeField] private float timerAmount = 1;

    [Header("Normal Attack Values")]
    [SerializeField] private float normalAttackPostureDamage = 2;
    [SerializeField] private float normalAttackMultiplier = 0.3f;

    private CombatController combatController;

    private Sequencer attackSequencer;

    private LineRenderer boltLineRenderer;

    private Transform boltObject;

    private Rigidbody boltRigidbody;
    private Transform Player => GameManager.Instance.Player.transform;
    private Rigidbody playerRigidbody => GameManager.Instance.Player.GetComponent<Rigidbody>();

    bool inProgress;

    private float timer;

    private AudioSource source;

    //Accessors

    public bool InProgress { get { return inProgress; } }

    public bool TimerIsUp { get { return Util.CheckTimer(timer); } }

    //Constants
    private Vector3 HOOKRESETPOSITION = new(40f, 0, 0);

    private const float GRAPPLEDISTANCE = 1.5f;

    private void Start()
    {
        combatController = GetComponentInParent<CombatController>();

        boltLineRenderer = GetComponent<LineRenderer>();

        attackSequencer = GetComponent<Sequencer>();

        boltObject = transform.GetChild(0);

        boltRigidbody = boltObject.GetComponent<Rigidbody>();

        source = transform.parent.GetComponent<AudioSource>();

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
    /// <param name="isGrappleable"></param>
    public void StartGrapple(Vector3 hitPoint, bool isGrappleable, GrappleSurface surface)
    {
        //Disable firing harpoon
        inProgress = true;

        StartCoroutine(ShootGrapple(hitPoint, isGrappleable, surface));
    }
    /// <summary>
    /// Lerps bobber to target position
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="hitPoint"></param>
    /// <param name="isGrappleable"></param>
    /// <returns></returns>
    IEnumerator ShootGrapple(Vector3 hitPoint, bool isGrappleable, GrappleSurface surface)
    {
        boltRigidbody.isKinematic = false;

        //Unparent bolt
        boltObject.parent = null;

        //Move bolt to hitpoint
        while (Vector3.Distance(hitPoint, boltObject.position) > GRAPPLEDISTANCE)
        {
            //boltObject.transform.position = Vector3.MoveTowards(boltObject.transform.position, hitPoint.point, reelInSpeed * Time.deltaTime);

            boltRigidbody.velocity = reelInSpeed * (hitPoint - boltObject.position).normalized;

            yield return null;
        }

        //Stop movement
        boltRigidbody.velocity = Vector3.zero;

        //Set final position
        boltObject.position = hitPoint;

        //Determine if player can grapple to surface or not
        if (isGrappleable)
        {
            StartCoroutine(GrapplePlayer(hitPoint, surface));
        }
        else
        {
            StartCoroutine(RetractGrapple());
        }
    }

    /// <summary>
    /// Lerps player to target position
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="hitPoint"></param>
    /// <returns></returns>
    IEnumerator GrapplePlayer(Vector3 hitPoint, GrappleSurface surface)
    {
        FloatEvent distance;

        distance.FloatValue = Vector3.Distance(Player.position, hitPoint);

        //Increase FOV
        fov_EventChannel.CallEvent(new());

        source.Play();

        //Move player to hitpoint
        while (Vector3.Distance(hitPoint, Player.position) > GRAPPLEDISTANCE)
        {
            playerRigidbody.velocity = grappleForce * (hitPoint - Player.position).normalized;

            yield return null;
        }

        
        if (surface == GrappleSurface.damageable)
        {
            //Starts timer
            timer = Time.time + timerAmount;

            //Stop velocity
            playerRigidbody.velocity = Vector3.zero;

            //Play attack effects
            attackSequencer.InitializeSequence();

            //Play attack animation
            combatController.Attack();

            //Deal damage
            damage_EventChannel.CallEvent(new(GameManager.Instance.PlayerDamage * normalAttackMultiplier));
        }
        //If weakpoint, deal damage
        else if (surface == GrappleSurface.weakPoint)
        {
            //Stop velocity
            playerRigidbody.velocity = Vector3.zero;

            //Play attack effects
            attackSequencer.InitializeSequence();

            //Play attack animation
            combatController.Attack();

            //Deal damage
            damage_EventChannel.CallEvent(new(GameManager.Instance.PlayerDamage));
            VoidEvent ctx;
            weakPoint_EventChannel.CallEvent(ctx);
        }

        ResetBolt();
    }

    /// <summary>
    /// Lerps bobber back to starting position
    /// </summary>
    /// <param name="totalTime"></param>
    /// <returns></returns>
    IEnumerator RetractGrapple()
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
    public void Retract()
    {
        StopAllCoroutines();

        StartCoroutine(RetractGrapple());
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

        inProgress = false;

        source.Stop();
    }

    /// <summary>
    /// Called when dashing
    /// </summary>
    /// <param name="ctx"></param>
    public void CallRetract(VoidEvent ctx)
    {
        Retract();
    }
}

