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

/// <summary>
/// Struct containing information related to boss distances for attacks
/// </summary>
[System.Serializable]
public class BossAttackInformation
{
    public BossAttackInformation()
    {
        meleeAttacks = new List<string>();
        rangedAttacks = new List<string>();
    }

    [Tooltip("Distance boss can initiate a melee attack from")]
    public float meleeDistance;
    [Tooltip("Distance that the boss will be forced to iniate a ranged attack from")]
    public float maxDistance;
    [Tooltip("Maximum ammount of time a boss must wait before being forced to attack")]
    public float waitTime;
    [Tooltip("Minimum amount of time a boss must wait before attacking again")]
    public float gracePeriod;
    [Tooltip("List of melee attack triggers for the boss")]
    public List<string> meleeAttacks;
    [Tooltip("List of ranged attack trigger for the boss")]
    public List<string> rangedAttacks;
}

public class BossAI : MonoBehaviour
{
    [SerializeField] BossAttackInformation bossInformation;
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }

    [SerializeField] private AIState currentState;

    [Tooltip("State Order: Idle, Walk, Attack, Stagger, tbc")]
    [SerializeField] List<AIState> bossStates = new List<AIState>();

    public AIState _CurrentState { get { return currentState; } }

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

    public void ToIdleState()
    {
        SwitchState(States.IdleState);
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

    public BossAttackInformation BossInformation
    {
        get { return bossInformation; }
    }
}

/* How to create a new Boss:
 * 1. Make a new animation controller
 * 2. Make a new animation event controller
 * 3. Create new states to suit the needs of the boss and assign the states to the list on this object
 * 4. Create custom animations and assign anim events
*/