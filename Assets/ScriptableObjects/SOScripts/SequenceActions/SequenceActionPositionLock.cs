using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Position Lock")]
public class SequenceActionPositionLock : SequencerAction
{
    [SerializeField] private BoolEventChannel bool_EventChannel;

    [SerializeField] private bool lockPosition = true;

    private Rigidbody rb => GameManager.Instance.Player.gameObject.GetComponent<Rigidbody>();

    public override void Initialize(GameObject obj)
    {

    }

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        Debug.Log("PLAYED!");

        if (lockPosition)
        {
            bool_EventChannel.CallEvent(new(true));
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            bool_EventChannel.CallEvent(new(false));
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        yield return null;
    }
}
