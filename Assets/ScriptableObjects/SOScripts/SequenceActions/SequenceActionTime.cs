using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Slow Time")]
public class SequenceActionTime : SequencerAction
{
    [SerializeField] AnimationCurve slowTimeCurve;
    
    [SerializeField] private float duration;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;

            Time.timeScale = slowTimeCurve.Evaluate(elapsedTime);

            yield return null;
        }
    }
}
