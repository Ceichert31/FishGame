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
        // Predict future position
        Vector3 predictedPosition = playerPosition + playerRigidbody.velocity * leadPercentage;

        // Calculate direction to predicted position
        Vector3 directionToPredicted = (predictedPosition - bossTransform.position).normalized;

        // Smoothly rotate toward predicted position
        Quaternion targetRotation = Quaternion.LookRotation(directionToPredicted);
        bossTransform.rotation = Quaternion.Slerp(bossTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        predictionDirection = directionToPredicted;
        Debug.Log("on");
    }

    public void SetRotationSpeed(float rotationSpeed) => this.rotationSpeed = rotationSpeed;

    private void OnDrawGizmos()
    {
        Debug.DrawRay(bossTransform.position, predictionDirection * 10, Color.black);
    }
}
