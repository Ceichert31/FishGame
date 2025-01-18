using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private FloatEventChannel maxPlayerHealth_EventChannel;
    [SerializeField] private FloatEventChannel currentPlayerHealth_EventChannel;
    [SerializeField] private VoidEventChannel transition_EventChannel;
    [SerializeField] private VoidEventChannel signalToBoss_EventChannel;
    [SerializeField] private AudioPitcherSO hurtAudio;

    [SerializeField] private float maxHealth = 100f;

    private FloatEvent currentHealth;

    private AudioSource source;

    private Sequencer damageSequencer;

    private bool isVulnerable = true; 

    private void Start()
    {
        currentHealth.FloatValue = maxHealth;

        //Set max health for UI
        maxPlayerHealth_EventChannel.CallEvent(new(maxHealth));

        source = GetComponent<AudioSource>();

        damageSequencer = GetComponent<Sequencer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isVulnerable) return;

        if (other.gameObject.TryGetComponent(out IProjectile projectileInstance))
        {
            DealDamage(projectileInstance.ProjectileDamage);

            projectileInstance.DeleteProjectile();
            return;
        }

        if (other.gameObject.TryGetComponent(out IMeele meeleInstance))
        {
            IParryable parryState = (IParryable)meeleInstance;

            if (parryState.Parried) return;
            DealDamage(meeleInstance.AttackDamage);
        }
    }

    /// <summary>
    /// Damages player and updates UI
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamage(float damage)
    {
        //Play damage effects
        damageSequencer.InitializeSequence();

        hurtAudio.Play(source);

        //Calculate new health
        currentHealth.FloatValue -= damage;

        //Update UI
        currentPlayerHealth_EventChannel.CallEvent(currentHealth);

        //Player Death
        if (currentHealth.FloatValue <= 0) 
        {
            transition_EventChannel.CallEvent(new());
            signalToBoss_EventChannel.CallEvent(new());
        }
    }

    /// <summary>
    /// Makes the player invulnerable for a set duration
    /// </summary>
    /// <param name="ctx"></param>
    public void SetIFrames(FloatEvent ctx)
    {
        isVulnerable = false;

        Invoke(nameof(ResetIFrames), ctx.FloatValue);
    }

    /// <summary>
    /// Makes the player vulnerable 
    /// </summary>
    /// <param name="ctx"></param>
    private void ResetIFrames()
    {
        isVulnerable = true;
    }

    public void CallResetIFrames(VoidEvent ctx) => ResetIFrames();

    public void ResetPlayerHealth(VoidEvent ctx)
    {
        //Reset current health
        currentHealth.FloatValue = maxHealth;

        //Update UI
        currentPlayerHealth_EventChannel.CallEvent(currentHealth);
    }
}