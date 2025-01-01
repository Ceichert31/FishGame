using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Sequencer Actions/Wait For Cutscene")]
public class SequencerActionWaitForCutscene : SequencerAction
{
    public float cutsceneLength;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        yield return new WaitForSeconds(cutsceneLength);
    }
}
