using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private float healthUpdateSpeed = 5f;

    [SerializeField] private float tempHealthUpdateSpeed = 3f;

    private Image tempHealthBar;

    private Image playerHealthBar;

    private float maxHealth;

    private const float MAXFILLAMOUNT = 1f;

    // Start is called before the first frame update
    void Start()
    {
        tempHealthBar = transform.GetChild(0).GetComponent<Image>();

        playerHealthBar = transform.GetChild(1).GetComponent<Image>();
    }

    public void SetMaxHealth(FloatEvent ctx)
    {
        maxHealth = ctx.FloatValue;

        playerHealthBar.fillAmount = MAXFILLAMOUNT;

        tempHealthBar.fillAmount = MAXFILLAMOUNT;
    }

    public void UpdateHealthBar(FloatEvent ctx)
    {
        float normalizedTarget = ctx.FloatValue / maxHealth;

        StartCoroutine(AnimateHealthBar(normalizedTarget));
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