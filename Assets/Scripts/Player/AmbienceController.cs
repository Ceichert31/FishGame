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

    /// <summary>
    /// Switches current background ambience
    /// </summary>
    /// <param name="ctx"></param>
    public void ChangeAmbience(AudioEvent ctx)
    {
        //Bootstrap case
        if (source.clip == ctx.audioClip) return;

        //Cache the previous audio clip before changing
        previousClip = new(source.clip, source.pitch, source.volume);

        //Load new music
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
        LoadAudio(previousClip);
    }

    private void LoadAudio(AudioEvent ctx)
    {
        source.clip = ctx.audioClip;

        source.volume = ctx.volume;

        source.Play();
    }
}
