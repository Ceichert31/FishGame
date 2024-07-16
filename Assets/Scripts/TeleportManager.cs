using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private ModeEventChannel mode_EventChannel;

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

        mode_EventChannel.TriggerEvent();
    }

    /// <summary>
    /// Returns the player to the last saved position
    /// </summary>
    public void ReturnToSavedPos()
    {
        Player.position = lastPosition.position;

        mode_EventChannel.TriggerEvent();
    }
}
