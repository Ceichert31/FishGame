using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Reward Key")]
public class KeyEventSequencerAction : SequencerAction
{
    [SerializeField] private KeyEventChannel key_EventChannel;

    [SerializeField] private KeyEvent keyEvent;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        key_EventChannel.CallEvent(keyEvent);

        yield return null;
    }
}
