using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IInteract
{
    public TextEvent Prompt => textPrompt;

    [SerializeField] private TextEvent textPrompt;

    private Animator animator;

    private bool isUnlocked;

    private void Start()
    {
        animator = GetComponent<Animator>();
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

        isUnlocked = true;
    }
}
