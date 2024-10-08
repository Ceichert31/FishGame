using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public enum States
{
    IdleState,
    WalkState,
    AttackState,
    StaggerState,
    FleeState,
}

public class BossAI : MonoBehaviour
{
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }

    [SerializeField] private AIState currentState;

    [Tooltip("State Order: Idle, Walk, Attack, Stagger, tbc")]
    [SerializeField] List<AIState> bossStates = new List<AIState>();

    private void Awake()
    {

        Agent = transform.parent.GetComponent<NavMeshAgent>();

        Animator = transform.parent.GetComponent<Animator>();

        InitializeDefaultState();
    }

    /// <summary>
    /// Sets the current state to the default state
    /// </summary>
    private void InitializeDefaultState()
    {
        currentState = bossStates[(int)States.WalkState];

        currentState.EnterStateController(this);
    }

    private void Update()
    {
        currentState?.ExecuteState(this);
    }

    /// <summary>
    /// Switches current state
    /// </summary>
    /// <param name="newState"></param>
    public void SwitchState(States newState)
    {
        if((int)newState > bossStates.Count -1)
        {
            throw new System.Exception("You are trying to enter a state that is out of the bounds of this AI's list");
        }

        currentState.ExitState(this);

        currentState = bossStates[(int)newState];

        currentState.EnterStateController(this);
    }


    //DevTools

    [ContextMenu("TransitionToWalkState")]
    public void ToWalkState()
    {
        SwitchState(States.WalkState);
    }

    //Immidiate
    public void ToStaggerState(VoidEvent voidEvent)
    {
        SwitchState(States.StaggerState);
    }


    public List<AIState> BossStates
    {
        get { return bossStates; }
    }

    private void OnDisable()
    {
        foreach(AIState state in bossStates)
        {
            state.UnCall();
        }
    }

}

/* How to create a new Boss:
 * 1. Make a new animation controller
 * 2. Make a new animation event controller
 * 3. Create new states to suit the needs of the boss and assign the states to the list on this object
 * 4. Create custom animations and assign anim events
*/

