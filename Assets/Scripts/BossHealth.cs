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

    [Header("Death Variables")]

    [SerializeField] Animator bossAnimator;

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
        if (currentHealth.FloatValue <= 0)
        {
            //Triggers A death animation
            bossAnimator.SetTrigger("Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentHealth.FloatValue <= 0)
        {
            return;
        }
        Debug.Log(other.gameObject.name);

        //If collision object is parried projectile, deal damage
        if (other.gameObject.TryGetComponent(out IProjectile projectileInstance))
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
    /// Calls disableBoss from void event channel
    /// </summary>
    /// <param name="ctx"></param>
    public void DisableBoss(VoidEvent ctx)
    {
        DisableBoss();
    }

    /// <summary>
    /// Destroys boss if player dies
    /// </summary>
    /// <param name="ctx"></param>
    void DisableBoss()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }


    /// <summary>
    /// Death Event Order:
    /// 1. Death animation is triggered whenever the enemy looses all of its health
    /// 2. Death animation calls an animation event that then calls this function
    /// 3. Death Sequencer goes throught its processes
    /// 4. Player is transitioned back to overworld
    /// 5. Boss is disabled
    /// </summary>
    public void ProcedeWithDeath()
    {
        deathSequencer.InitializeSequence();

        //Returns player
        return_EventChannel.CallEvent(new());

        DisableBoss();
    }
}


