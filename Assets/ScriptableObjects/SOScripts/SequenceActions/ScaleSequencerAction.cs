using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Scale")]
public class ScaleSequencerAction : SequencerAction
{
    [SerializeField] private Vector3 targetScale;

    [SerializeField] private float changeScaleSpeed;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        GameObject targetObject = ctx.transform.GetChild(0).gameObject;

        //Scale down
        while (targetObject.transform.localScale != targetScale)
        {
            targetObject.transform.localScale = Vector3.MoveTowards(targetObject.transform.localScale, targetScale, changeScaleSpeed * Time.deltaTime);

            yield return null;
        }

        //Set to target scale
        targetObject.transform.localScale = targetScale;
    }
}
