using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceController : MonoBehaviour
{

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void ChangeAmbience(AudioEvent ctx)
    {
        source.Stop();

        source.clip = ctx.audioClip;

        source.volume = ctx.volume;

        source.pitch = ctx.pitch;

        source.Play();
    }
}
