using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Time Stop")]
public class TimeSequencerAction : SequencerAction
{
    [SerializeField] private float duration = 1.5f;
    public override IEnumerator StartSequence(Sequencer ctx)
    {
        Debug.Log("ENTERED");

        Time.timeScale = 0;

        float waitTime = 0;

        while (waitTime < duration)
        {
            waitTime += Time.unscaledDeltaTime;

            Debug.Log(waitTime);

            yield return null;
        }

        Time.timeScale = 1;
    }
}
