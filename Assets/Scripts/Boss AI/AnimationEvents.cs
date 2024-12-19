using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;

public enum ParticleTypes
{
    StaggerParticles,
    ParryParticles,
}

public abstract class AnimationEvents : MonoBehaviour
{
    public delegate void ActiveBehavior();
    public ActiveBehavior activeBehavior;

    [SerializeField] Transform modelsTransform;
    [SerializeField] AudioPitcherSO attackAudio;

    protected IBossWalkBehavior bossWalkBehavior;
    protected IBossLookAtPlayer bossLookAtPlayer;


    [Tooltip("Order: StaggerParticles, ParryParticles")]
    [SerializeField] List<ParticleSystem> particles = new List<ParticleSystem>();


    protected AudioSource source;
    protected Animator bossAnimator;
    protected BossHealth bossHealth;
    protected BossAI bossAi;
    protected BossPosture bossPosture;

    
    // Start is called before the first frame update
    void Awake()
    {
        OnAwake();
    }

    public virtual void OnAwake()
    {
        try
        {
            bossAi = transform.GetChild(0).GetComponent<BossAI>();
        }
        catch
        {
            throw new System.Exception("There is no BossAi under this object or your scripts object is not in the right place");
        }

        bossAnimator = GetComponent<Animator>();
        bossPosture = modelsTransform.GetComponent<BossPosture>();
        bossHealth = bossPosture.GetComponent<BossHealth>();

        bossWalkBehavior = transform.GetChild(0).GetComponent<IBossWalkBehavior>();
        bossLookAtPlayer = transform.GetChild(0).GetComponent<IBossLookAtPlayer>();

        //temp add so delegate is never null
        activeBehavior += () => { };

    }

    /// <summary>
    /// Called by animator
    /// </summary>
    public void PlayParticles(ParticleTypes particleTypes)
    {
        particles[(int)particleTypes].Play();
    }

    /// <summary>
    /// Called by animator
    /// </summary>
    public void StopParticles(ParticleTypes particleTypes)
    {
        particles[(int)particleTypes].Stop();

    }

    public void EndAttacking()
    {
        IAttackState attackState = (IAttackState)bossAi.BossStates[(int)States.AttackState];
        attackState.Attacking = false;
        Debug.Log("attack ended");
    }

    //public abstract void TeleportBoss();

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
        transform.position = bossPosture.transform.position;
        bossPosture.transform.localPosition = Vector3.zero;
        Invoke(nameof(ReEnableApplyRootMotion), 0.5f);
    }

    public void PlayAttackIndicatorAudio()
    {
        attackAudio.Play(source);
    }

    void ReEnableApplyRootMotion()
    {
        bossAnimator.applyRootMotion = true;
    }

    public void TriggerDeath()
    {
        bossHealth.ProcedeWithDeath();
    }

    /// <summary>
    /// Allows specific attacks to give or take away root motion
    /// 0 = false
    /// 1 = true
    /// </summary>
    /// <param name="value"></param>
    public void SetRootMotion(int value)
    {
        bossAnimator.applyRootMotion = value == 1 ? true : false;
    }

    /// <summary>
    /// Removes all events from active behavior(This has the possibility to cause a null reference error, requiers testing)
    /// </summary>
    public void RemoveAllListeners()
    {
        activeBehavior = null;
        activeBehavior += () => { };
    }

    /// <summary>
    /// 0: Boss Starts Looking At Player
    /// 1: Boss Stops Looking At The Player
    /// 2: Boss Starts Moving Toward The Player
    /// 3: Boss Stops Moving Toward The Player
    /// </summary>
    /// <param name="behavior"></param>
    public virtual void UpdateBossActiveBehavior(int behavior)
    {
        switch (behavior)
        {
            case 0:
                activeBehavior += bossLookAtPlayer.LookAtPlayer;
                break;
            case 1:
                activeBehavior -= bossLookAtPlayer.LookAtPlayer;
                break;
            case 2:
                activeBehavior += bossWalkBehavior.MoveBehavior;
                break;
            case 3:
                activeBehavior -= bossWalkBehavior.MoveBehavior;
                break;
        }
    }

    private void Update()
    {
        activeBehavior();
    }
}
/* -How To Use Active Behavior-
 * 
 * Purpose: To be able to tell the boss when to do certain time based actions during animations
 * 
 * Coding Standereds: Assigning a behavior and De-Assigning a behvaior should be done in subsequent order, 
 *                    and should also be added above the method in a comment
 * 
 * Variables:
 * delegate ActiveBehvaior: public delegate
 * ActiveBehavior activeBehavior: instance of delegate used to assign active methods to. Called in update to execute behaviors
 * 
 * Methods:
 * virutal UpdateBossActiveBehavior(int behavior): Virtual method allowing us to pass in numbers to dictate what behavior we want to execute.
 *                                                 Method is virtual to allow child classes to make their own behavior based on specific needs.
 * 
 */