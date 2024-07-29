using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Void Event")]
public class VoidSequenceAction : SequencerAction
{
    [SerializeField] private VoidEventChannel void_EventChannel;

    [SerializeField] private VoidEvent voidEvent;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        void_EventChannel.CallEvent(voidEvent);

        yield return null;
    }

}
