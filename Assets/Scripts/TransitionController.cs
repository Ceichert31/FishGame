using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [Header("Teleport Manager Settings")]
    [SerializeField] private Transform arenaTransform;

    private Transform lastPosition;
    private Transform Player => GameManager.Instance.Player.transform;

    private Sequencer transitionSequencer;


    private void Awake()
    {
        transitionSequencer = GetComponent<Sequencer>();
    }

    /// <summary>
    /// Moves player between arena and last position
    /// </summary>
    /// <param name="ctx"></param>
    public void TransportPlayer(BoolEvent ctx)
    {
        if (ctx.Value) 
        {
            //Save players last position
            lastPosition = Player;

            //Teleport player to arena
            Player.position = arenaTransform.position;
        }
        else
        {
            //Return player
            Player.position = lastPosition.position;
        }
        
    }

    /// <summary>
    /// Starts Sequencer for transition to arena
    /// </summary>
    /// <param name="ctx"></param>
    public void StartTransition(HookedEvent ctx)
    {
        transitionSequencer.InitializeSequence();
    }
}
