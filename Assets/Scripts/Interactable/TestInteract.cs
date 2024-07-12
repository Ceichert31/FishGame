using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour, IInteract
{
    [SerializeField] private string textPrompt = "Test!";

    public string Prompt { get { return textPrompt; } }


    private Sequencer testSequencer;

    private void Start()
    {
        testSequencer = GetComponent<Sequencer>();
    }

    public void Interact()
    {
        testSequencer.InitializeSequence();
    }
    public void ExitInteract()
    {
        Debug.Log("EXIT!");
    }
}
