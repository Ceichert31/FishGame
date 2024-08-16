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

    /// <summary>
    /// Switches current background ambience
    /// </summary>
    /// <param name="ctx"></param>
    public void ChangeAmbience(AudioEvent ctx)
    {
        //Bootstrap case
        if (source.clip == ctx.audioClip) return;

        source.Stop();

        source.clip = ctx.audioClip;

        source.volume = ctx.volume;

        source.pitch = ctx.pitch;

        source.Play();
    }
}
