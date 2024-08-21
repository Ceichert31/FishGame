using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private FloatEventChannel maxPlayerHealth_EventChannel;
    [SerializeField] private FloatEventChannel currentPlayerHealth_EventChannel;
    [SerializeField] private VoidEventChannel transition_EventChannel;
    [SerializeField] private AudioPitcherSO hurtAudio;

    [SerializeField] private float maxHealth = 100f;

    private FloatEvent currentHealth;

    private AudioSource source;

    private void Start()
    {
        currentHealth.FloatValue = maxHealth;

        //Set max health for UI
        maxPlayerHealth_EventChannel.CallEvent(new(maxHealth));

        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Parryable")) return;

        if (other.gameObject.TryGetComponent(out IProjectile projectileInstance))
        {
            DealDamage(projectileInstance.ProjectileDamage);

            projectileInstance.DisableProjectile();
        }
    }

    /// <summary>
    /// Damages player and updates UI
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamage(float damage)
    {
        hurtAudio.Play(source);

        //Calculate new health
        currentHealth.FloatValue -= damage;

        //Update UI
        currentPlayerHealth_EventChannel.CallEvent(currentHealth);

        //Player Death
        if (currentHealth.FloatValue <= 0) 
        {
            transition_EventChannel.CallEvent(new());
        }
    }

    public void ResetPlayerHealth(VoidEvent ctx)
    {
        //Reset current health
        currentHealth.FloatValue = maxHealth;

        //Update UI
        currentPlayerHealth_EventChannel.CallEvent(currentHealth);
    }
}