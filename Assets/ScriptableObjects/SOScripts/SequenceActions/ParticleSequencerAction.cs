using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Particle")]
public class ParticleSequencerAction : SequencerAction
{
    private ParticleSystem particleSystem;

    public override void Initialize(GameObject obj)
    {
        particleSystem = obj.GetComponent<ParticleSystem>();
    }

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        particleSystem.Play();

        yield return null;
    }
}
