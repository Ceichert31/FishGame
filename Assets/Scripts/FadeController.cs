using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [Header("Fade References")]
    [SerializeField] private Image fadeImage;

    [Header("Fade Settings")]
    [SerializeField] private AnimationCurve fadeCurve;

    [SerializeField] private float fadeDuration = 1f;

    private Color fadeAlpha;

    private void Start()
    {
        fadeAlpha = fadeImage.color;
    }

    public void SetFade(FadeEvent ctx)
    {
        fadeAlpha = new Color(ctx.fadeColor.r, ctx.fadeColor.g, ctx.fadeColor.b, fadeAlpha.a);

        if (!ctx.fadeIn)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            fadeAlpha.a = fadeCurve.Evaluate(elapsedTime);

            fadeImage.color = fadeAlpha;

            yield return null;
        }

        fadeAlpha.a = fadeCurve.Evaluate(fadeDuration);

        fadeImage.color = fadeAlpha;
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = fadeDuration;

        while (elapsedTime > 0)
        {
            elapsedTime -= Time.unscaledDeltaTime;

            fadeAlpha.a = fadeCurve.Evaluate(elapsedTime);

            fadeImage.color = fadeAlpha;

            yield return null;
        }

        fadeAlpha.a = fadeCurve.Evaluate(0);

        fadeImage.color = fadeAlpha;
    }
}
