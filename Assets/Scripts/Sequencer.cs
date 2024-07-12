using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    public List<SequencerAction> sequenceActions;

    Coroutine instance = null;

    private void Awake()
    {
        foreach (SequencerAction action in sequenceActions) 
            action.Initialize(gameObject);
    }

    [ContextMenu("Test")]
    public void InitializeSequence()
    {
        if (instance != null) return;

        instance = StartCoroutine(nameof(ExecuteSequence));   
    }

    private IEnumerator ExecuteSequence()
    {
        foreach (SequencerAction action in sequenceActions)
            yield return StartCoroutine(action.StartSequence(this));
        
        instance = null;
    }
}
