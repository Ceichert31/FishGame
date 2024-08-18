using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Set Ambience")]
public class SetAmbienceSequencerObject : SequencerAction
{
    [SerializeField] private AudioEventChannel audioEventChannel;

    [SerializeField] private AudioEvent newAudioEvent;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        audioEventChannel.CallEvent(newAudioEvent);

        yield return null;  
    }
}
