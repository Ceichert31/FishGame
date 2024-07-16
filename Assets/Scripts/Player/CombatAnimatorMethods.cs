using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimatorMethods : MonoBehaviour
{
    private CombatController combatController;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        combatController = GetComponentInParent<CombatController>();
    }
}
