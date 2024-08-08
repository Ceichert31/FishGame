using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TestInteract : MonoBehaviour, IInteract
{
    [SerializeField] private IntEventChannel testEvet;

    [SerializeField] private string textPrompt = "Test!";

    public string Prompt { get { return textPrompt; } }

    private IntEvent intEvent;


    private Sequencer testSequencer;

    private void Start()
    {
        //intEvent.Value = 5;
        testSequencer = transform.GetChild(0).GetComponent<Sequencer>();
    }

    public void Interact()
    {
        //testSequencer.InitializeSequence();
        //cameraShake_EventChannel.CallEvent(new(1f
        //testEvet.CallEvent(intEvent);
        testSequencer.InitializeSequence();
    }
    public void ExitInteract()
    {
        Debug.Log("EXIT!");
    }
}
