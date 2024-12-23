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
    float currentTime;
    bool teleporting;

    private Coroutine instance;

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
        if (Time.time > currentTime)
        {
            currentTime = Time.time + timeUntilNextMovement;

            if (instance != null) return;

            instance = StartCoroutine(nameof(Charge));
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
        
    }

    IEnumerator Charge()
    {
        animationEvents.UpdateBossActiveBehavior(1);
        float timeElapsed = 0;
        while (timeElapsed < chargeTime)
        {
            timeElapsed += Time.deltaTime;
            bossTransform.position += dashSpeed * Time.deltaTime * bossTransform.forward;
            yield return null;
        }
        instance = null;
        animationEvents.UpdateBossActiveBehavior(0);
    }
}
