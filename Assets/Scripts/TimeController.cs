using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private AnimationCurve slowTimeCurve;

    [SerializeField] private float duration = 0.3f;

    private bool slowTime;

    public void SetTime(FloatEvent targetTime)
    {
        slowTime = !slowTime;

        if (slowTime)
        {
            Time.timeScale = targetTime.FloatValue / 20f;
            Debug.Log(Time.timeScale);
            //StartCoroutine(SlowTime(targetTime.FloatValue));
        }
        else
        {
            Debug.Log("Resumed");
            StartCoroutine(ResumeTime());
        }
    }

    IEnumerator SlowTime(float distance)
    {
        float elapsedTime = 0f;

        //Grapple has a maximum range of 20, so if we divide by 20 we
        //Get a value that can affect time scale
        slowTimeCurve.keys[1].value = distance / 20f;

        Debug.Log(distance / 20f);

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

        Debug.Log(Time.timeScale);
    }
}
