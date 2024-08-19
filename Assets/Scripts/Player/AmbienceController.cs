using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    private AudioSource source;

    private AudioEvent previousClip;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void CachePreviousAudio()
    {
        previousClip.audioClip = source.clip;

        previousClip.pitch = source.pitch;

        previousClip.volume = source.volume;

    }

    /// <summary>
    /// Switches current background ambience
    /// </summary>
    /// <param name="ctx"></param>
    public void ChangeAmbience(AudioEvent ctx)
    {
        //Bootstrap case
        if (source.clip == ctx.audioClip) return;

        CachePreviousAudio();

        source.clip = ctx.audioClip;

        source.volume = ctx.volume;

        source.pitch = ctx.pitch;

        source.Play();
    }

    /// <summary>
    /// Plays previous audio played
    /// </summary>
    /// <param name="ctx"></param>
    public void LoadPreviousClip(VoidEvent ctx)
    {
        source.clip = previousClip.audioClip;

        source.volume = previousClip.volume;

        source.pitch = previousClip.pitch;

        source.Play();
    }
}
