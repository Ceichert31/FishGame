using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private FloatEventChannel health_EventChannel;
    [SerializeField] private FloatEventChannel maxHealth_EventChannel;
    [SerializeField] private VoidEventChannel return_EventChannel;

    [Header("Boss Health Settings")]
    [SerializeField] private int bossMaxHealth = 100;

    private FloatEvent currentHealth;

    private void Start()
    {
        //Set health to max
        currentHealth.FloatValue = bossMaxHealth;

        //Initialize max health
        maxHealth_EventChannel.CallEvent(currentHealth);
    }

    /// <summary>
    /// Updates the boss's health with input value
    /// </summary>
    /// <param name="ctx"></param>
    public void UpdateHealth(FloatEvent ctx)
    {
        currentHealth.FloatValue -= ctx.FloatValue;

        health_EventChannel.CallEvent(currentHealth);

        //Fish health hits zero
        if (currentHealth.FloatValue <= 0 )
        {
            return_EventChannel.CallEvent(new());
        }
    }
}
