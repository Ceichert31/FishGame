using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Movement")]
public class SequencerActionMovement : SequencerAction
{
    [SerializeField] private float movementForce;

    [SerializeField] private float movementDuration;

    [SerializeField] private ForceMode forceMode;

    [SerializeField] private Vector3 movementDirection;

    [SerializeField] private bool directionFoward;

    private Rigidbody rb;

    public override void Initialize(GameObject obj)
    {
        rb = obj.GetComponent<Rigidbody>();

        if (directionFoward )
            movementDirection = obj.transform.forward;
    }

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        float currentTime = 0f;

        while (currentTime < movementDuration)
        {
            currentTime += Time.deltaTime;

            rb.AddForce(movementForce * Time.unscaledDeltaTime * movementDirection, forceMode);

            yield return null;
        }
    }
}
