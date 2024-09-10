using System.Collections;
using System.Collections.Generic;
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
}
