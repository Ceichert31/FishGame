using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Bool Event")]
public class BoolSequenceAction : SequencerAction
{
    [SerializeField] private BoolEventChannel bool_EventChannel;

    [SerializeField] private BoolEvent boolEvent;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        bool_EventChannel.CallEvent(boolEvent);
    
        yield return null;
    }
}
