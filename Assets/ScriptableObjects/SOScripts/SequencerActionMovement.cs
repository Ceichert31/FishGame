using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Movement")]
public class SequencerActionMovement : SequencerAction
{
    [SerializeField] private float movementForce;

    [SerializeField] private ForceMode forceMode;

    private Vector3 movementDirection;

    private Rigidbody rb;

    public override void Initialize(GameObject obj)
    {
        rb = obj.GetComponent<Rigidbody>();

        movementDirection = obj.transform.forward;
    }

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        rb.AddForce(movementForce * Time.unscaledDeltaTime * movementDirection, forceMode);
        yield return new WaitForSeconds(1); 
    }
}
