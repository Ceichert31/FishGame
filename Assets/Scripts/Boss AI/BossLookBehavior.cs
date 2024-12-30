using HelperMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLookBehavior : MonoBehaviour, IBossLookAtPlayer
{
    Vector3 playerPosition => GameManager.Instance.Player.transform.position;
    Rigidbody playerRigidbody => GameManager.Instance.Player.GetComponent<Rigidbody>();
    Transform bossTransform => transform.parent;


    [SerializeField] float rotationSpeed = 1;

    [SerializeField] float leadPercentage = 2;

    Vector3 predictionDirection;

    public void LookAtPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Util.VectorNoY(playerPosition) - Util.VectorNoY(bossTransform.position));
        bossTransform.rotation = Quaternion.Slerp(bossTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void PredictPlayerPosition()
    {
        predictionDirection = (Util.VectorNoY(playerPosition) + Util.VectorNoY(playerRigidbody.velocity)) * leadPercentage;
        bossTransform.rotation = Quaternion.Slerp(bossTransform.rotation, Quaternion.LookRotation(-predictionDirection), rotationSpeed * Time.deltaTime);
    }

    public void SetRotationSpeed(float rotationSpeed) => this.rotationSpeed = rotationSpeed;

    private void OnDrawGizmos()
    {
        Debug.DrawRay(bossTransform.position, predictionDirection * 10, Color.black);
    }
}
