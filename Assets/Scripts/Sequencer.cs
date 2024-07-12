using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    public List<SequencerAction> sequenceActions;

    private void Awake()
    {
        foreach (SequencerAction action in sequenceActions) 
            action.Initialize(gameObject);
    }

    public void InitializeSequence() => StartCoroutine(nameof(ExecuteSequence));

    private IEnumerator ExecuteSequence()
    {
        foreach (SequencerAction action in sequenceActions)
        {
            yield return StartCoroutine(action.StartSequence(this));
        }
    }
}
