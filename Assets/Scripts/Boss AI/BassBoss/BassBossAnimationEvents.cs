using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassBossAnimationEvents : AnimationEvents
{
    private BassBossMoveBehavoir bassBossMoveBehavior;

    [SerializeField] private FloatEventChannel cameraShakeChannel;

    [SerializeField] private GameObject explosionObject;

    private IProjectileSpawner projectileSpawner;
    private ParticleSystem explosionParticle;
    private AudioSource explosionAudio;

    private void Start()
    {
        bassBossMoveBehavior = (BassBossMoveBehavoir)bossWalkBehavior;

        explosionParticle = explosionObject.GetComponent<ParticleSystem>();
        explosionAudio = explosionObject.GetComponent<AudioSource>();

        transform.GetChild(0).TryGetComponent(out projectileSpawner);
    }


    /// <summary>
    /// 4: Constantly move toward Player
    /// 5: Deassign Constant movment Method
    /// </summary>
    public override void UpdateBossActiveBehavior(int behavior)
    {
        base.UpdateBossActiveBehavior(behavior);

        switch (behavior)
        {
            case 4:
                activeBehavior += bassBossMoveBehavior.ConstantMovement;
                break;
            case 5:
                activeBehavior -= bassBossMoveBehavior.ConstantMovement;
                break;
        }
    }

    public void PlayExplosionParticle()
    {
        explosionParticle.Play();
        explosionAudio.Play();
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
