using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Position Lock")]
public class SequenceActionPositionLock : SequencerAction
{
    [SerializeField] private bool lockPosition = true;

    private Rigidbody rb => GameManager.Instance.Player.gameObject.GetComponent<Rigidbody>();

    public override void Initialize(GameObject obj)
    {

    }

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        if (lockPosition)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        yield return null;
    }
}
