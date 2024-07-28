using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Text")]
public class TextSequencerAction : SequencerAction
{
    [SerializeField] private TextEventChannel text_EventChannel;

    [SerializeField] private TextEvent textEvent;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        text_EventChannel.CallEvent(textEvent);
        
        yield return null;
    }
}
