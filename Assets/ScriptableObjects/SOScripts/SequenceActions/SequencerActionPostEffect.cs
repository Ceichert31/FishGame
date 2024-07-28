using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Post Effect")]
public class SequencerActionPostEffect : SequencerAction
{
    [SerializeField] private PostEffectEventChannel post_EventChannel;

    [SerializeField] private PostEvent postProfile;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        post_EventChannel.CallEvent(postProfile);

        yield return null;
    }
}
