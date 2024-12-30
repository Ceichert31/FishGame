using System.Collections;
using System.Collections.Generic;
using HelperMethods;
using UnityEngine;

public class BassBossMoveBehavoir : MonoBehaviour, IBossWalkBehavior
{
    Transform bossTransform => transform.parent;
    [SerializeField] AnimationEvents animationEvents;

    [Header("Variables for controlling unique movement")]
    [SerializeField] float slowDownAmmount = 2;
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float chargeTime = 3f;
    [SerializeField] float timeUntilNextMovement = 7f;
    [SerializeField] float stopChargeDistance = 3.5f;
    float currentTime;
    bool teleporting;

    private BossLookBehavior lookBehavior;

    private Coroutine instance;
    private Transform Player => GameManager.Instance.Player.transform;

    public bool Teleporting
    {
        get { return teleporting; }
        set { teleporting = value; }
    }


    private void Awake()
    {
        currentTime = timeUntilNextMovement;
        lookBehavior = GetComponent<BossLookBehavior>();
    }

    public void MoveBehavior()
    {
        if (Time.time > currentTime)
        {
            currentTime = Time.time + timeUntilNextMovement;
            lookBehavior.SetRotationSpeed(1);

            if (instance != null) return;

            instance = StartCoroutine(nameof(Charge));
        }
    }

    public void ConstantMovement()
    {
        if (Util.DistanceNoY(Player.position, transform.position) < stopChargeDistance)
        {
            instance = null;
            return;
        }

        bossTransform.position += dashSpeed * Time.deltaTime * bossTransform.forward;
    }

    /// <summary>
    /// Teleports the boss to the player after the player has gone farther then boss allowed
    /// </summary>
    public void TeleportBehavior()
    {
        //Temp implimentation, will do more ltr

        /*teleporting = true;
        bossTransform.position = GameManager.Instance.Player.transform.position;
        teleporting = false;*/

    }

    /// <summary>
    /// Method designed to charge the player if they are too far away
    /// </summary>
    public void ChargePlayer()
    {
        
    }

    IEnumerator Charge()
    {
        lookBehavior.SetRotationSpeed(4);
        float timeElapsed = 0;
        while (timeElapsed < chargeTime)
        {
            //Break out of charge if player is close
            if (Util.DistanceNoY(Player.position, transform.position) < stopChargeDistance)
            {
                instance = null;
                break;
            }

            timeElapsed += Time.deltaTime;
            bossTransform.position += dashSpeed * Time.deltaTime * bossTransform.forward;
            yield return null;
        }
        instance = null;
    }
}
