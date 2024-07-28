using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Camera Shake")]
public class SequenceActionCameraShake : SequencerAction
{
    [SerializeField] private FloatEventChannel shake_EventChannel;

    [SerializeField] private FloatEvent shakeDuration;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        shake_EventChannel.CallEvent(shakeDuration);

        yield return null;
    }
}
