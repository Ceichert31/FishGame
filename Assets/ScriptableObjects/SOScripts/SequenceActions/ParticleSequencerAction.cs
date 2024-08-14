using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Particle")]
public class ParticleSequencerAction : SequencerAction
{
    public override IEnumerator StartSequence(Sequencer ctx)
    {
        ctx.GetComponent<ParticleSystem>().Play();

        yield return null;
    }
}
