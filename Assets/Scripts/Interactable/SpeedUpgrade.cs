using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private float speedIncreaseAmount = 0.2f;

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
            GameManager.Instance.IncreasePlayerSpeed(speedIncreaseAmount);

            //Play collection sequence
            collectedSequencer.InitializeSequence();
        }
    }
}
