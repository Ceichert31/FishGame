using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Disable Object")]
public class DisableObjectSequenceAction : SequencerAction
{
    private GameObject target;

    public override void Initialize(GameObject obj)
    {
        target = obj.transform.GetChild(0).gameObject;
    }

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        target.SetActive(false);

        yield return null;
    }
}
