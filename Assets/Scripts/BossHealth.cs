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

    [SerializeField] private bool rewardsKey;

    private FloatEvent currentHealth;

    private Sequencer deathSequencer;

    private void Start()
    {
        //Set health to max
        currentHealth.FloatValue = bossMaxHealth;

        //Initialize max health
        maxHealth_EventChannel.CallEvent(currentHealth);

        //Start whatever sequence this boss has
        gameObject.GetComponent<Sequencer>().InitializeSequence();

        deathSequencer = transform.GetChild(0).GetComponent<Sequencer>();
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
            deathSequencer.InitializeSequence();

            //Returns player
            return_EventChannel.CallEvent(new());

            Destroy(transform.parent.gameObject);
        }
    }

    /// <summary>
    /// Destroys boss if player dies
    /// </summary>
    /// <param name="ctx"></param>
    public void DisableBoss(VoidEvent ctx)
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
