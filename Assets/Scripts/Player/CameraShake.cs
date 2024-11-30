using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Screen Shake Settings")]
    [SerializeField] private AnimationCurve shakeCurve;

    public void StartShaking(FloatEvent ctx) => StartCoroutine(Shake(ctx.FloatValue));

    IEnumerator Shake(float duration)
    {
        Vector3 startPosition = transform.localPosition;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            Vector3 shakeAmount = Random.insideUnitSphere * shakeCurve.Evaluate(elapsedTime);

            shakeAmount = new (shakeAmount.x, shakeAmount.y, 0);

            transform.localPosition = startPosition + shakeAmount;

            yield return null;
        }

        transform.localPosition = startPosition;
    }

    [ContextMenu("TEST")]
    public void Test()
    {
        StartCoroutine(Shake(1f));
    }

}
