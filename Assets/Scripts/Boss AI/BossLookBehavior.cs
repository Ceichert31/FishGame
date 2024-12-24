using HelperMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLookBehavior : MonoBehaviour, IBossLookAtPlayer
{
    Vector3 playerPosition => GameManager.Instance.Player.transform.position;
    Transform bossTransform => transform.parent;


    [SerializeField] float rotationSpeed = 1;

    public void LookAtPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Util.VectorNoY(playerPosition) - Util.VectorNoY(bossTransform.position));
        bossTransform.rotation = Quaternion.Slerp(bossTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetRotationSpeed(float rotationSpeed) => this.rotationSpeed = rotationSpeed;
}
