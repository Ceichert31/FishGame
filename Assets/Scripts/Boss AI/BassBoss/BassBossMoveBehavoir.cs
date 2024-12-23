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
    [SerializeField] float dashSpeed = 25f;
    float timeUntilNextMovement = 2;
    float currentTime;
    bool teleporting;

    //Charging Variables
    float chargeSpeed = 30;

    public bool Teleporting
    {
        get { return teleporting; }
        set { teleporting = value; }
    }


    private void Awake()
    {
        currentTime = timeUntilNextMovement;
        animationEvents.UpdateBossActiveBehavior(0);
        animationEvents.UpdateBossActiveBehavior(2);
    }

    public void MoveBehavior()
    {
        Debug.Log("Moving!!");
        if (Time.time > currentTime)
        {
            //animationEvents.UpdateBossActiveBehavior(1);
            currentTime = Time.time + timeUntilNextMovement;
            bossTransform.position += dashSpeed * Time.deltaTime * bossTransform.forward;
        }
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
        bossTransform.position += bossTransform.forward * chargeSpeed * Time.deltaTime;

        if (!Util.IsLookingAtTarget(bossTransform, GameManager.Instance.Player.transform, 0))
        {
            Debug.Log("Fish passed up player");
            animationEvents.UpdateBossActiveBehavior(5);
        }
    }
}
