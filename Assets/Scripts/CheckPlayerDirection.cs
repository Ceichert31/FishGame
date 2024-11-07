using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerDirection : MonoBehaviour
{
    private Transform player => GameManager.Instance.Player.transform;

    // Update is called once per frame
    void Update()
    {

        Vector3 playerDir = (transform.position - player.position).normalized;
        Vector3 direction = (transform.position - player.forward).normalized;
        Debug.DrawRay(transform.position, transform.position * 10, Color.blue);
        Debug.DrawRay(transform.position, playerDir * 10, Color.yellow);
        Debug.DrawRay(transform.position, direction * 10, Color.red);
        Debug.Log(Vector3.Dot(playerDir, player.forward.normalized));
    }
}
