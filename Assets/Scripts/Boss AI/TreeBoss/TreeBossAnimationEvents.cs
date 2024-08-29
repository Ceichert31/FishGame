using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBossAnimationEvents : MonoBehaviour
{
    [SerializeField] BossAI bossAi;
    [SerializeField] BossPosture bossPosture;
    Animator bossAnimator;

    [SerializeField] ParticleSystem staggerParticle;
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
        TreeAttackState attackState = (TreeAttackState)bossAi.BossStates[(int)States.AttackState];
        attackState.Attacking = false;
        Debug.Log("attack ended");
    }

    public void TeleportTreeBoss()
    {
        Debug.Log("called");
        FleeState fleeState = (FleeState)bossAi.BossStates[(int)States.FleeState];
        fleeState.TeleportFish();
    }

    public void ResetPosture()
    {
        bossPosture.ResetPosture();
    }


    /// <summary>
    /// IMPORTANT FOR ANIMATION
    /// Allows the fish object to move in animation and have the movements carry over into the game properly
    /// </summary>
    public void SetParentToChild()
    {
        bossAnimator.applyRootMotion = false;
        bossAnimator.transform.position = bossPosture.transform.position;
        bossPosture.transform.localPosition = Vector3.zero;
        Invoke(nameof(ReEnableApplyRootMotion), 0.5f);
    }

    void ReEnableApplyRootMotion()
    {
        bossAnimator.applyRootMotion = true;
    }
}
