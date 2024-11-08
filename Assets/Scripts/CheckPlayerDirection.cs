using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerDirection : MonoBehaviour
{
    private Transform player => GameManager.Instance.Player.transform;

    public float PlayerDirection()
    {
        Vector3 playerDir = (transform.position - player.position).normalized;
        return Vector3.Dot(playerDir, player.forward.normalized);
    }
}
