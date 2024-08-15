using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceEventTrigger : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private AudioEventChannel ambience_EventChannel;

    [SerializeField] private AudioEvent audioEvent;

    private const int PLAYERLAYER = 6;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PLAYERLAYER)
        {
            ambience_EventChannel.CallEvent(audioEvent);
        }
    }
}
