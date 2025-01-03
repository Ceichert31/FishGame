using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashBarUI : MonoBehaviour
{
    float currentDashValue;

    float maxDashValue = 2;

    [SerializeField] Image dashBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentDashValue += Time.deltaTime;

        currentDashValue = Mathf.Clamp(currentDashValue, 0, maxDashValue);

        dashBar.fillAmount = currentDashValue / maxDashValue;

        if(dashBar.fillAmount < .3)
        {
            dashBar.color = Color.red;
        }
        else if(dashBar.fillAmount < .7)
        {
            dashBar.color = Color.yellow;
        }
        else
        {
            dashBar.color = Color.green;
        }
    }

    public void ResetDash(FloatEvent ctx)
    {
        maxDashValue = ctx.FloatValue;

        currentDashValue = 0;
    }
}
