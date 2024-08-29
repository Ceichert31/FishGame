using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Sequencer Actions/Rotate Player")]
public class PlayerRotationSequencerAction : SequencerAction
{
    [SerializeField] private Vector3 targetRotation;

    private Transform Player => GameManager.Instance.Player.transform;

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        Player.eulerAngles = targetRotation;

        yield return null;
    }
}
