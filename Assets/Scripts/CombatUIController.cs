using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Enables or disables combat UI
    /// </summary>
    /// <param name="ctx"></param>
    public void SetCombatUI(BoolEvent ctx)
    {
        animator.SetBool("IsCombat", ctx.Value);
    }
}
