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
    [SerializeField] Transform modelsTransform;
    [SerializeField] AudioPitcherSO attackAudio;


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

    public abstract void EndAttacking();

    public abstract void TeleportBoss();

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
}