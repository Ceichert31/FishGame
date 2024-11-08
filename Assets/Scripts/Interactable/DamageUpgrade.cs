using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private int damageIncreaseAmount = 5;

    private Sequencer collectedSequencer;

    private VanishableObject vanishingCabin;

    private const int PLAYERLAYER = 6;

    private void Start()
    {
        collectedSequencer = transform.parent.GetComponent<Sequencer>();

        vanishingCabin = transform.parent.parent.GetComponent<VanishableObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PLAYERLAYER)
        {
            //Set flag for cabin to disapear
            vanishingCabin.VanishFlag();

            //Add to damage
            GameManager.Instance.IncreaseDamage(damageIncreaseAmount);

            //Play collection sequence
            collectedSequencer.InitializeSequence();
        }
    }
}
