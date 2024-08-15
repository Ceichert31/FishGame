using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Play Audio")]
public class PlayAudioSequencerAction : SequencerAction
{
    [SerializeField] private AudioPitcherSO pitcher;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        AudioSource source = ctx.GetComponent<AudioSource>();

        source.Stop();

        pitcher.Play(source);

        yield return null;
    }
}
