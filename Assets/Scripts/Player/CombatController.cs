using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    [Header("Hook Settings")]
    [SerializeField] private float parryDelay = 0.8f;

    [SerializeField] private float grappleRange = 20f;

    [SerializeField] private float grappleFireTime = 3f;

    private Transform bobberHolder;

    private Transform bobberObject;

    private float parryCooldown;

    private Animator hookAnimator;

    private bool canGrapple;

    private void Awake()
    {
        hookAnimator = transform.GetChild(1).GetComponent<Animator>();

        bobberHolder = transform.GetChild(0).GetChild(1);

        bobberObject = transform.GetChild(0).GetChild(1).GetChild(0);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * grappleRange, Color.red);
    }

    /// <summary>
    /// Fires raycast to detect if there is a weakpoint
    /// </summary>
    void FireGrapple(InputAction.CallbackContext ctx)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, grappleRange))
        {
            StartCoroutine(ShootGrapple(grappleFireTime, hitInfo.point));

            if (hitInfo.collider.CompareTag("Weakpoint"))
            {
                //Lerp player to point
                Debug.Log("Grapple!");
                canGrapple = true;
            }
            else
            {
                //Retract grapple
                canGrapple = false;
                Debug.Log("Can't Grapple!");
            }
        }
    }

    IEnumerator ShootGrapple(float totalTime, Vector3 hitPoint)
    {
        bobberObject.parent = null;

        Vector3 startPos = bobberObject.position;

        float timeElapsed = 0f;

        while (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;

            bobberObject.position = Vector3.Lerp(startPos, hitPoint, timeElapsed / totalTime);

            yield return null;
        }
        bobberObject.position = hitPoint;

        if (canGrapple)
        {

        }
        else
        {
            StartCoroutine(RetractGrapple(grappleFireTime));
        }
    }

    IEnumerator RetractGrapple(float totalTime)
    {
        Vector3 startPos = bobberObject.position;

        float timeElapsed = 0f;

        while (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;

            bobberObject.position = Vector3.Lerp(startPos, bobberHolder.position, timeElapsed / totalTime);

            yield return null;
        }
        bobberObject.position = bobberHolder.position;

        bobberObject.parent = bobberHolder;
    }

    /// <summary>
    /// Plays the parry animation
    /// </summary>
    /// <param name="ctx"></param>
    void Parry(InputAction.CallbackContext ctx) 
    {
        //Time gate to prevent queued parries
        if (parryCooldown <= Time.time)
        {
            parryCooldown = Time.time + parryDelay;

            hookAnimator.SetTrigger("Parry");
        }
    }   

    /// <summary>
    /// Subscribes functions to the correct controls
    /// </summary>
    /// <param name="ctx"></param>
    public void InitializeControls(InputEvent ctx)
    {
        ctx.Action.Combat.Disable();

        ctx.Action.Combat.ReelIn.performed += Parry;

        ctx.Action.Combat.Fire.performed += FireGrapple;
    }
}
