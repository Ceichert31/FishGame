using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Sequencer Actions/PrintAction")]
public class IntSequencerAction : SequencerAction
{
    [SerializeField] IntEventChannel print_EventChannel;

    [SerializeField] IntEvent intEvent;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        print_EventChannel.CallEvent(intEvent);
        yield return null;
    }
}
