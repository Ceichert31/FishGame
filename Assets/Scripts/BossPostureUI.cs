using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPostureUI : MonoBehaviour
{
    [Header("Boss Health UI Settings")]
    [Tooltip("How fast the health bar moves to update")]
    [SerializeField] private float postureUpdateSpeed = 2f;

    private Image postureBar;

    private float maxPosture;

    private void Start()
    {
        postureBar = GetComponent<Image>();
    }

    public void SetMaxPosture(FloatEvent ctx)
    {
        maxPosture = ctx.FloatValue;

        postureBar.fillAmount = 0;
    }

    /// <summary>
    /// Updates the posture UI with a new value
    /// </summary>
    /// <param name="ctx"></param>
    public void UpdatePostureBar(FloatEvent ctx)
    {
        float normalizedTarget = ctx.FloatValue / maxPosture;

        StartCoroutine(AnimatePostureBar(normalizedTarget));
    }

    /// <summary>
    /// Animates the bar to slowly move towards target value
    /// </summary>
    /// <param name="targetAmount"></param>
    /// <returns></returns>
    IEnumerator AnimatePostureBar(float targetAmount)
    {
        while (postureBar.fillAmount != targetAmount)
        {
            postureBar.fillAmount = Mathf.MoveTowards(postureBar.fillAmount, targetAmount, postureUpdateSpeed * Time.deltaTime);

            yield return null;
        }

        postureBar.fillAmount = targetAmount;
    }
}
