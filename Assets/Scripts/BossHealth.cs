using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private FloatEventChannel healthUI_EventChannel;
    [SerializeField] private FloatEventChannel maxHealthUI_EventChannel;
    [SerializeField] private VoidEventChannel return_EventChannel;

    [Header("Boss Health Settings")]
    [SerializeField] private int bossMaxHealth = 100;

    [SerializeField] private float parryDamageMultiplier = 0.2f;

    private FloatEvent currentHealth;

    private FloatEvent damage;

    private Sequencer deathSequencer;

    private void Start()
    {
        //Set health to max
        currentHealth.FloatValue = bossMaxHealth;

        //Initialize max health
        maxHealthUI_EventChannel.CallEvent(currentHealth);

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

        //Updates health UI
        healthUI_EventChannel.CallEvent(currentHealth);

        //Fish health hits zero
        if (currentHealth.FloatValue <= 0 )
        {
            deathSequencer.InitializeSequence();

            //Returns player
            return_EventChannel.CallEvent(new());

            Destroy(transform.parent.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (currentHealth.FloatValue <= 0)
        {
            return;
        }

        //If collision object is parried projectile, deal damage
        if (other.TryGetComponent(out IProjectile projectileInstance))
        {
            if (projectileInstance.IsParried)
            {
                //Calculate parry damage
                damage.FloatValue = projectileInstance.ProjectileDamage * parryDamageMultiplier;

                //Deal parry damage
                UpdateHealth(damage);

                projectileInstance.DeleteProjectile();

                //Get collsion point and play special effects
            }
        }
    }

    /// <summary>
    /// Destroys boss if player dies
    /// </summary>
    /// <param name="ctx"></param>
    public void DisableBoss(VoidEvent ctx)
    {
        Destroy(gameObject.transform.parent.parent.gameObject);
    }
}
