using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [Header("Boss Health UI Settings")]
    [Tooltip("How fast the health bar moves to update")]
    [SerializeField] private float healthUpdateSpeed = 2f;

    private Image healthBar;

    private float maxHealth;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }
    
    public void SetMaxHealth(FloatEvent ctx)
    {
        maxHealth = ctx.FloatValue;
    }

    /// <summary>
    /// Starts health bar animation
    /// </summary>
    /// <param name="ctx"></param>
    public void UpdateHealthBar(FloatEvent ctx)
    {
        float normalizedTarget = ctx.FloatValue / maxHealth;

        StartCoroutine(AnimateHealthBar(normalizedTarget));
    }

    /// <summary>
    /// Moves health bar to target health value
    /// </summary>
    /// <param name="targetAmount"></param>
    /// <returns></returns>
    IEnumerator AnimateHealthBar(float targetAmount)
    {
        //Decrease healthbar target amount
        while (healthBar.fillAmount != targetAmount)
        {
            healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, targetAmount, healthUpdateSpeed * Time.deltaTime);

            yield return null;
        }

        healthBar.fillAmount = targetAmount;
    }
}
