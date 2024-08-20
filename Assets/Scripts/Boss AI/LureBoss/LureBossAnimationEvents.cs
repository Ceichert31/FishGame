using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

public class LureBossAnimationEvents : MonoBehaviour
{
    [SerializeField] BossAI bossAi;
    Animator bossAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            bossAi = GetComponentInChildren<BossAI>();
        }
        catch
        {
            throw new System.Exception("There is no BossAi under this object");
        }
        bossAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndAttacking()
    {
        AttackState attackState =  (AttackState)bossAi.BossStates[(int)States.AttackState];
        attackState.Attacking = false;
    }

    public void NotStaggerTrigger()
    {
        bossAnimator.SetTrigger("NotStaggered");
    }
}
