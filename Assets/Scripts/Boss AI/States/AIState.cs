using UnityEngine;
using HelperMethods;

public abstract class AIState : ScriptableObject
{
    protected Vector3 Player => GameManager.Instance.Player.transform.position;
    public Transform bossTransform;

    /// <summary>
    /// Each State Needs Own private Called Boolean, accessed in this script using this to Either Initalize 
    /// </summary>
    protected abstract bool Called { get; set; }

    /// <summary>
    /// This method is called when first entering a new state
    /// DO NOT CALL THIS FROM THE STATE MACHIENE
    /// </summary>
    /// <param name="ctx"></param>
    public abstract void EnterState(BossAI ctx);

    /// <summary>
    /// This method is called when enetering the state for the first time
    /// DO NOT CALL THIS FROM THE STATE MACHIENE
    /// </summary>
    /// <param name="ctx"></param>
    public abstract void InitalizeState(BossAI ctx);

    /// <summary>
    /// This method is called whenever entering a state, chooses which method that is called
    /// </summary>
    /// <param name="ctx"></param>
    public void EnterStateController(BossAI ctx)
    {
        if(Called)
        {
            EnterState(ctx);
            return;
        }

        AssignBossTransform(ctx);
        InitalizeState(ctx);
        EnterState(ctx);
        Called = true;
    }

    public void AssignBossTransform(BossAI ctx)
    {
        bossTransform = Util.TryGetParent(ctx.transform);
    }

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

    private void OnDisable()
    {
        Called = false;
    }
}
