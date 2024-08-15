using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Audio Event Channel")]
public class AudioEventChannel : GenericEventChannel<AudioEvent> {}

[System.Serializable]
public struct AudioEvent
{
    public AudioClip audioClip;

    public float volume;

    public float pitch;

    public AudioEvent(AudioClip audioClip, float volume, float pitch)
    {
        this.audioClip = audioClip;
        this.volume = volume;
        this.pitch = pitch;
    }
}
