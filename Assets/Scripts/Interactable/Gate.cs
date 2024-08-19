using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IInteract
{
    [SerializeField] private AudioPitcherSO gateCreakAudio;

    public TextEvent Prompt => textPrompt;

    [SerializeField] private TextEvent textPrompt;

    private Animator animator;

    private AudioSource source;

    private bool isUnlocked;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();

        source = GetComponentInParent<AudioSource>();
    }

    public void ExitInteract()
    {
        
    }

    public void Interact()
    {
        if (isUnlocked) return;

        //Check if player has key
        if (!GameManager.Instance.HasGateKey) return;

        //If player has key open gate
        animator.SetTrigger("OpenGate");

        gateCreakAudio.Play(source);

        isUnlocked = true;

        textPrompt.TextPrompt = string.Empty;
    }
}
