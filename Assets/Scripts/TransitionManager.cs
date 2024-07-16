using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private ModeEventChannel mode_EventChannel;
    [SerializeField] private VoidEventChannel void_EventChannel;

    [Header("Teleport Manager Settings")]
    [SerializeField] private Transform arenaTransform;

    private Transform lastPosition;

    private Transform Player => GameManager.Instance.Player.transform;

    /// <summary>
    /// Teleports the player into the boss arena
    /// </summary>
    /// <param name="ctx"></param>
    public void TeleportToArena(HookedEvent ctx)
    {
        //Save players last position
        lastPosition = Player;

        //Teleport player to arena
        Player.position = arenaTransform.position;

        //Switch to combat mode
        mode_EventChannel.TriggerEvent(true);

        //Disable line renderer
        void_EventChannel.CallEvent(new());
    }

    /// <summary>
    /// Returns the player to the last saved position
    /// </summary>
    public void ReturnToSavedPos()
    {
        //Return player
        Player.position = lastPosition.position;

        //Switch to fishing mode
        mode_EventChannel.TriggerEvent(false);
    }
}
