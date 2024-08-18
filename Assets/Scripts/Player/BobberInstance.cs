using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberInstance : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private AudioPitcherSO bobberSplooshAudio;

    [Header("Bobber Settings")]
    [SerializeField] private int waterLayer = 4;

    [SerializeField] bool inWater;

    private AudioSource source;

    //resets necisary variables on enable
    private void OnEnable()
    {
        inWater = false;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == waterLayer)
        {
            bobberSplooshAudio.Play(source);

            if (collision.gameObject.TryGetComponent(out SpawnPool waterInstance))
            {
                waterInstance.StartFishing();
                inWater = true;
            }
        }
    }

    public bool InWater
    {
        get { return inWater; }
    }
}
