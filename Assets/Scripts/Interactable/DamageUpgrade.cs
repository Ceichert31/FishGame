using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private int damageIncreaseAmount = 5;

    private Sequencer collectedSequencer;

    private const int PLAYERLAYER = 6;

    private void Start()
    {
        collectedSequencer = GetComponentInParent<Sequencer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PLAYERLAYER)
        {
            //Add to damage
            GameManager.Instance.IncreaseDamageStat(damageIncreaseAmount);

            //Play collection sequence
            collectedSequencer.InitializeSequence();
        }
    }
}
