using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AudioSO : ScriptableObject
{
    protected abstract void Play(AudioSource source);
}
