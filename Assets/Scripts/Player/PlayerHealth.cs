using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private FloatEventChannel maxPlayerHealth_EventChannel;
    [SerializeField] private FloatEventChannel currentPlayerHealth_EventChannel;

    [SerializeField] private float maxHealth = 100f;

    private FloatEvent currentHealth;

    private void Awake()
    {
        currentHealth.FloatValue = maxHealth;

        //Set max health for UI
        maxPlayerHealth_EventChannel.CallEvent(currentHealth);
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

    public void DealDamage(float damage)
    {
        currentHealth.FloatValue -= damage;

        currentPlayerHealth_EventChannel.CallEvent(currentHealth);

        if (currentHealth.FloatValue <= 0) 
        {
            //Death
        }
    }
}
