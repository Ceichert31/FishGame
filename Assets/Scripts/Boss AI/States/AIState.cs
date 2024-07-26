using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    protected Vector3 Player => GameManager.Instance.Player.transform.position;

    /// <summary>
    /// This method is called when first entering a new state
    /// </summary>
    /// <param name="ctx"></param>
    public abstract void EnterState(BossAI ctx);

    /// <summary>
    /// This method is continously called while a state is in progress
    /// </summary>
    /// <param name="ctx"></param>
    public abstract void ExecuteState(BossAI ctx);

    /// <summary>
    /// This method is called right before switching to the next state
    /// </summary>
    /// <param name="ctx"></param>
    public abstract void ExitState(BossAI ctx);
}
