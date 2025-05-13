using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensAnimatorMethods : MonoBehaviour
{
    [SerializeField] private Transform lens;
    [SerializeField] private Transform lensTint;
    [SerializeField] private float tweenDuration = 0.35f;

    //Audio Stuff
    [SerializeField] private AudioPitcherSO xraySFX;
    private AudioSource source;

    const float Z_SCALE = 7.031852f;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void OpenLens()
    {
        lens.DOScaleZ(Z_SCALE, tweenDuration);
        lensTint.DOScaleZ(Z_SCALE, tweenDuration);
        xraySFX.Play(source);
    }
    public void CloseLens()
    {
        lens.DOScaleZ(0f, tweenDuration);
        lensTint.DOScaleZ(0f, tweenDuration);
        xraySFX.Play(source);
    }
}
