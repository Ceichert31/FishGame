using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequencer Actions/Animator")]
public class AnimatorSequencerAction : SequencerAction
{
    [SerializeField] private string animationID;

    private Animator animator;

    public override void Initialize(GameObject obj)
    {
        animator = obj.GetComponent<Animator>();
    }

    public override IEnumerator StartSequence(Sequencer ctx)
    {
        animator.SetTrigger(animationID);

        yield return null;
    }
}
