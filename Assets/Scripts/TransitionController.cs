using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [Header("Teleport Manager Settings")]
    [SerializeField] private Transform arenaTransform;

    private Vector3 lastPosition;
    private Transform Player => GameManager.Instance.Player.transform;

    private Sequencer arenaTransition;

    private Sequencer returnTransition;

    private void Awake()
    {
        arenaTransition = transform.GetChild(0).GetComponent<Sequencer>();

        returnTransition = transform.GetChild(1).GetComponent<Sequencer>();
    }

    /// <summary>
    /// Moves player between arena and last position
    /// </summary>
    /// <param name="ctx"></param>
    public void TransportPlayer(BoolEvent ctx)
    {
        if (ctx.Value) 
            TransportToArena();
        else
            ReturnPlayer();
    }

    private void TransportToArena()
    {
        //Save players last position
        lastPosition = Player.position;

        //Teleport player to arena
        Player.position = arenaTransform.position;
    }

    private void ReturnPlayer()
    {
        //Return player
        Player.position = lastPosition;
    }

    /// <summary>
    /// Starts Sequencer for transition to arena
    /// </summary>
    /// <param name="ctx"></param>
    public void StartArenaTransition(HookedEvent ctx)
    {
        arenaTransition.InitializeSequence();
    }

    public void StartReturnTransition(VoidEvent ctx)
    {
        returnTransition.InitializeSequence();
    }
}
