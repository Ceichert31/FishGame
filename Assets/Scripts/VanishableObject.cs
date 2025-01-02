using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CheckPlayerDirection))]
public class VanishableObject : MonoBehaviour
{
    [SerializeField] private float vanishDistance = 30f;
    private Vector3 player => GameManager.Instance.Player.transform.position;

    private CheckPlayerDirection directionChecker;

    private bool canVanish;

    private const float DOT_PRODUCT_RANGE = 0.15f;
    private void Start()
    {
        directionChecker = GetComponent<CheckPlayerDirection>();
    }

    /// <summary>
    /// Set flag for object to disapear
    /// </summary>
    public void VanishFlag()
    {
        canVanish = true;
    }

    private void Update()
    {
        //If flag is true
        if (!canVanish) return;

        //If player isn't looking at object
        if (directionChecker.PlayerDirection() > DOT_PRODUCT_RANGE) return;

        //If player is a certain distance away
        if (Vector3.Distance(transform.position, player) > vanishDistance)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

}
