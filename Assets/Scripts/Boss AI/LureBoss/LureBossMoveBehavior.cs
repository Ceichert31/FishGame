using HelperMethods;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class LureBossMoveBehavior : MonoBehaviour, IBossWalkBehavior
{
    Transform bossTransform => transform.parent;

    [Header("Variables for controlling unique movement")]
    [SerializeField] float initalMoveAmmount = 10;
    [SerializeField] float slowDownAmmount = 2;
    float timeUntilNextMovement = 1;
    float currentTime;
    float currentMoveAmmount;
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
        currentMoveAmmount = initalMoveAmmount;
    }

    public void MoveBehavior()
    {
        if (currentMoveAmmount <= 0)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = timeUntilNextMovement;
                currentMoveAmmount = initalMoveAmmount;
            }

            return;
        }

        bossTransform.position += bossTransform.forward * currentMoveAmmount * Time.deltaTime;

        currentMoveAmmount -= slowDownAmmount * Time.deltaTime;
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

        if(!Util.IsLookingAtTarget(bossTransform, GameManager.Instance.Player.transform, 0))
        {
            Debug.Log("Fish passed up player");
            //Will Add Exit Condition
        }
    }
}
