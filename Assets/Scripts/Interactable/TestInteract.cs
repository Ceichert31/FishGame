using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TestInteract : MonoBehaviour, IInteract
{
    [SerializeField] private FloatEventChannel cameraShake_EventChannel;
    [SerializeField] private PostEffectEventChannel post_EventChannel;

    [SerializeField] private string textPrompt = "Test!";

    [SerializeField] private VolumeProfile profile;

    public string Prompt { get { return textPrompt; } }


    private Sequencer testSequencer;

    private void Start()
    {
        testSequencer = GetComponent<Sequencer>();
    }

    public void Interact()
    {
        //testSequencer.InitializeSequence();
        //cameraShake_EventChannel.CallEvent(new(1f));

        profile.TryGet(out LiftGammaGain lift);
        lift.gamma.value = new Vector4(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f), lift.gamma.value.w);
        profile.TryGet(out ChromaticAberration chrome);
        chrome.intensity.value = Random.value;
        profile.TryGet(out WhiteBalance balance);
        balance.temperature.value = Random.Range(-100, 100);
        balance.tint.value = Random.Range(-100, 100);
        profile.TryGet(out LensDistortion distortion);
        distortion.intensity.value = Random.Range(-0.5f, 1f);
        profile.TryGet(out ColorAdjustments adjust);
        adjust.contrast.value = Random.Range(-100, 100);
        adjust.hueShift.value = Random.Range(-100, 100);
        adjust.saturation.value = Random.Range(-100, 100);
        adjust.postExposure.value = Random.Range(-100, 100);

        post_EventChannel.CallEvent(new(profile));
    }
    public void ExitInteract()
    {
        Debug.Log("EXIT!");
    }
}
