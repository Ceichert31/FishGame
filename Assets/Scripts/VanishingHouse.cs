using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CheckPlayerDirection))]
public class VanishingHouse : MonoBehaviour
{
    [SerializeField] private float vanishDistance = 30f;
    private Vector3 player => GameManager.Instance.Player.transform.position;

    private CheckPlayerDirection directionChecker;

    private bool hasGrabbedUpgrade;

    private void Start()
    {
        directionChecker = GetComponent<CheckPlayerDirection>();
    }

    public void PlayerGrabbedUpgrde()
    {
        hasGrabbedUpgrade = true;
    }

    private void Update()
    {
        if (!hasGrabbedUpgrade) return;

        if (directionChecker.PlayerDirection() > 0.5f) return;

        if (Vector3.Distance(transform.position, player) > vanishDistance)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

}
