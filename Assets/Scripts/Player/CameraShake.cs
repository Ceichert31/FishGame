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
        Vector3 startPosition = transform.position;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            transform.position = startPosition + (Random.insideUnitSphere * shakeCurve.Evaluate(elapsedTime));

            yield return null;
        }

        transform.position = startPosition;
    }
}
