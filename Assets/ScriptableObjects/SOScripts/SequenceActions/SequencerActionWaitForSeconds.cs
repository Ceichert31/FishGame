using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Wait For Seconds")]
public class SequencerActionWaitForSeconds : SequencerAction
{
    [SerializeField] private float waitForSeconds;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        yield return new WaitForSeconds(waitForSeconds);
    }
}
