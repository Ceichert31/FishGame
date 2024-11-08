using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private int damageIncreaseAmount = 5;

    private Sequencer collectedSequencer;

    private VanishingHouse vanishingHouse;

    private const int PLAYERLAYER = 6;

    private void Start()
    {
        collectedSequencer = transform.parent.GetComponent<Sequencer>();

        vanishingHouse = transform.parent.parent.GetComponent<VanishingHouse>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PLAYERLAYER)
        {
            vanishingHouse.PlayerGrabbedUpgrde();

            //Add to damage
            GameManager.Instance.IncreaseDamage(damageIncreaseAmount);

            //Play collection sequence
            collectedSequencer.InitializeSequence();
        }
    }
}
