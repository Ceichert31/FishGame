using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayerAudio : MonoBehaviour
{
    [SerializeField] private GameObjectEventChannel init_EventChannel;

    private void Start()
    {
        init_EventChannel.CallEvent(new(gameObject));
    }
}
