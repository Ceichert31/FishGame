using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Slow Time")]
public class SequenceActionTime : SequencerAction
{
    [SerializeField] private BoolEventChannel time_EventChannel;

    [SerializeField] private BoolEvent slowTime;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        time_EventChannel.CallEvent(slowTime);
    
        yield return null;
    }
}
