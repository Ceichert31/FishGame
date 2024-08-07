using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossNameController : MonoBehaviour
{
    private TextMeshProUGUI bossText;

    private void Start()
    {
        bossText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Sets boss's name to caught fish
    /// </summary>
    /// <param name="ctx"></param>
    public void SetBossName(HookedEvent ctx)
    {
        bossText.text = ctx.fishInstance.name;
    }
}
