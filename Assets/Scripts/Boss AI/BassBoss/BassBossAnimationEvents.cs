using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum BassSounds
{
    Bite,
    Swim,
}

public class BassBossAnimationEvents : AnimationEvents
{
    private BassBossMoveBehavoir bassBossMoveBehavior;

    [SerializeField] private FloatEventChannel cameraShakeChannel;

    [SerializeField] private GameObject explosionObject;

    [Header("Audio References")]
    [SerializeField] private AudioPitcherSO swimAudio;
    [SerializeField] private AudioPitcherSO biteAudio;

    private IProjectileSpawner projectileSpawner;
    private ParticleSystem explosionParticle;
    private AudioSource explosionAudio;
    private AudioSource mainSource;

    private void Start()
    {
        bassBossMoveBehavior = (BassBossMoveBehavoir)bossWalkBehavior;

        explosionParticle = explosionObject.GetComponent<ParticleSystem>();
        explosionAudio = explosionObject.GetComponent<AudioSource>();
        mainSource = GetComponent<AudioSource>();


        transform.GetChild(0).TryGetComponent(out projectileSpawner);
    }


    /// <summary>
    /// 6: Constantly move toward Player
    /// 7: Deassign Constant movment Method
    /// </summary>
    public override void UpdateBossActiveBehavior(int behavior)
    {
        base.UpdateBossActiveBehavior(behavior);

        switch (behavior)
        {
            case 6:
                activeBehavior += bassBossMoveBehavior.ConstantMovement;
                break;
            case 7:
                activeBehavior -= bassBossMoveBehavior.ConstantMovement;
                break;
        }
    }

    public void PlayExplosionParticle()
    {
        explosionParticle.Play();
        explosionAudio.Play();
    }

    public void PlayAudio(BassSounds audio)
    {
        switch (audio)
        {
            case BassSounds.Swim:
                swimAudio.Play(mainSource);
                break;
            case BassSounds.Bite:
                biteAudio.Play(mainSource);
                break;
        }
    }

    public void ShakeCamera(float duration)
    {
        cameraShakeChannel.CallEvent(new FloatEvent(duration));
    }
    public void FireProjectiles(int amount)
    {
        projectileSpawner.Spawn(amount);
    }
}
