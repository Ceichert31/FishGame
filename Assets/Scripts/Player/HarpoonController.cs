using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HarpoonController : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private VoidEventChannel fov_EventChannel;

    private CombatController combatController;

    private Sequencer attackSequencer;

    private LineRenderer boltLineRenderer;

    private Transform boltObject;
    private Transform Player => GameManager.Instance.Player.transform;

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
        boltObject.parent = null;

        while (Vector3.Distance(hitPoint.point, boltObject.position) > GRAPPLEDISTANCE)
        {
            boltObject.transform.position = Vector3.MoveTowards(boltObject.transform.position, hitPoint.point, reelInSpeed * Time.deltaTime);

            yield return null;
        }

        boltObject.position = hitPoint.point;

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
        fov_EventChannel.CallEvent(new());

        //attackSequencer.InitializeSequence();

        //Transform target = hitPoint.transform.GetChild(0);

        while (Vector3.Distance(hitPoint.point, Player.position) > GRAPPLEDISTANCE)
        {
            Player.position = Vector3.MoveTowards(Player.position, hitPoint.point, grappleForce * Time.deltaTime);

            if (Vector3.Distance(hitPoint.point, Player.position) > 30f)
            {
                ResetBolt();
            }

            yield return null;
        }

        combatController.Attack();
        attackSequencer.InitializeSequence();

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
            boltObject.transform.position = Vector3.MoveTowards(boltObject.transform.position, transform.position, reelInSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, boltObject.position) > 30f)
            {
                ResetBolt();
            }

            yield return null;
        }

        ResetBolt();
    }

    void ResetBolt()
    {
        StopAllCoroutines();

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

