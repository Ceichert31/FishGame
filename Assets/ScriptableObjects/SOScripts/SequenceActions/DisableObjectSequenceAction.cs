using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Disable Object")]
public class DisableObjectSequenceAction : SequencerAction
{
    public override IEnumerator StartSequence(Sequencer ctx)
    {
        ctx.transform.GetChild(0).gameObject.SetActive(false);

        yield return null;
    }
}
