using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Float Event")]
public class FloatEventSequencerAction : SequencerAction
{
    [SerializeField] private FloatEventChannel channel;

    [SerializeField] private FloatEvent floatEvent;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        channel.CallEvent(floatEvent);

        yield return null;
    }
}
