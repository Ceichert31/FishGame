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

    /*
    public IdleState idleState;
    public WalkState walkState;
    public AttackState attackState;
    public StaggerState staggerState;
    */

    private void Awake()
    {

        Agent = transform.parent.GetComponent<NavMeshAgent>();

        Animator = transform.parent.GetComponent<Animator>();

        InitializeDefaultState();

        
        /*for(int i = 0; i < states.Count; i++)
        {
            states[i] = new();
        }*/
    }

    /// <summary>
    /// Sets the current state to the default state
    /// </summary>
    private void InitializeDefaultState()
    {
        //currentState = walkState;
        currentState = bossStates[(int)States.WalkState];

        currentState.EnterStateController(this);
    }

    private void Update()
    {
        if (currentState != null) 
            currentState.ExecuteState(this);
    }

    /// <summary>
    /// Switches current state
    /// </summary>
    /// <param name="newState"></param>
    /*public void SwitchState(AIState newState)
    {
        currentState.ExitState(this);

        currentState = newState;

        currentState.EnterStateController(this);
    }*/

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
        //SwitchState(walkState);
        SwitchState(States.WalkState);
    }

    //Immidiate
    public void ToStaggerState(VoidEvent voidEvent)
    {
        Debug.Log("Enter stagger State");
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
