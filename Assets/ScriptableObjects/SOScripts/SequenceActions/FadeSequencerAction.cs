using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Fade Event")]
public class FadeSequencerAction : SequencerAction
{
    [SerializeField] private FadeEventChannel fade_EventChannel;

    [SerializeField] private FadeEvent fadeEvent;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        fade_EventChannel.CallEvent(fadeEvent);

        yield return null;
    }
}
