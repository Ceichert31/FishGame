using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour, IInteract
{
    [SerializeField] private FloatEventChannel cameraShake_EventChannel;

    [SerializeField] private string textPrompt = "Test!";

    public string Prompt { get { return textPrompt; } }


    private Sequencer testSequencer;

    private void Start()
    {
        testSequencer = GetComponent<Sequencer>();
    }

    public void Interact()
    {
        //testSequencer.InitializeSequence();
        cameraShake_EventChannel.CallEvent(new(1f));
    }
    public void ExitInteract()
    {
        Debug.Log("EXIT!");
    }
}
