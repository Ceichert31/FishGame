using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;
using static UnityEngine.ParticleSystem;

public class LureBossAnimationEvents : MonoBehaviour
{
    [SerializeField] BossAI bossAi;
    Animator bossAnimator;

    ParticleSystem staggerParticle;
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

        staggerParticle = transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Called by animator
    /// </summary>
    public void PlayParticles()
    {
        staggerParticle.Play();
    }

    /// <summary>
    /// Called by animator
    /// </summary>
    public void StopParticles()
    {
        staggerParticle.Stop();

        staggerParticle.Clear();
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
