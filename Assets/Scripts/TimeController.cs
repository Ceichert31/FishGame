using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private AnimationCurve slowTimeCurve;

    [SerializeField] private float duration = 0.3f;

    public void SetTime(BoolEvent ctx)
    {
        if (ctx.Value)
        {
            StartCoroutine(SlowTime());
        }
        else
        {
            StartCoroutine(ResumeTime());
        }
    }

    IEnumerator SlowTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            Time.timeScale = slowTimeCurve.Evaluate(elapsedTime);

            yield return null;
        }

        Time.timeScale = slowTimeCurve.Evaluate(duration);
    }

    IEnumerator ResumeTime()
    {
        float elapsedTime = duration;

        while (elapsedTime > 0)
        {
            elapsedTime -= Time.unscaledDeltaTime;

            Time.timeScale = slowTimeCurve.Evaluate(elapsedTime);

            yield return null;
        }

        Time.timeScale = slowTimeCurve.Evaluate(0);
    }
}
