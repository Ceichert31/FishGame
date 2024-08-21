using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private float healthUpdateSpeed = 5f;

    [SerializeField] private float tempHealthUpdateSpeed = 3f;

    [SerializeField] private float tempHealthMultiplier = 1.1f;

    private Image tempHealthBar;

    private Image playerHealthBar;

    private float maxHealth;

    private const float MAXFILLAMOUNT = 1f;

    // Start is called before the first frame update
    void Start()
    {
        tempHealthBar = transform.GetChild(0).GetComponent<Image>();

        playerHealthBar = transform.GetChild(1).GetComponent<Image>();

        playerHealthBar.fillAmount = MAXFILLAMOUNT;

        tempHealthBar.fillAmount = MAXFILLAMOUNT;
    }

    public void SetMaxHealth(FloatEvent ctx)
    {
        maxHealth = ctx.FloatValue;
    }

    public void UpdateHealthBar(FloatEvent ctx)
    {
        float normalizedTarget = ctx.FloatValue / maxHealth;

        StartCoroutine(AnimateTempHealthBar(normalizedTarget * tempHealthMultiplier));

        StartCoroutine(AnimateHealthBar(normalizedTarget));
    }

    IEnumerator AnimateTempHealthBar(float targetAmount)
    {
        while (tempHealthBar.fillAmount != targetAmount)
        {
            tempHealthBar.fillAmount = Mathf.MoveTowards(tempHealthBar.fillAmount, targetAmount, tempHealthUpdateSpeed * Time.deltaTime);

            yield return null;
        }

        tempHealthBar.fillAmount = targetAmount;
    }

    IEnumerator AnimateHealthBar(float targetAmount)
    {
        while (playerHealthBar.fillAmount != targetAmount)
        {
            playerHealthBar.fillAmount = Mathf.MoveTowards(playerHealthBar.fillAmount, targetAmount, healthUpdateSpeed * Time.deltaTime);

            yield return null;
        }

        playerHealthBar.fillAmount = targetAmount;
    }
}
