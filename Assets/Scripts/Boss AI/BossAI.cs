using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }

    [SerializeField] private AIState currentState;

    public IdleState idleState = new();
    public WalkState walkState = new();

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
        currentState = idleState;

        currentState.EnterState(this);
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
    public void SwitchState(AIState newState)
    {
        currentState.ExitState(this);

        currentState = newState;

        currentState.EnterState(this);
    }


    //DevTools

    [ContextMenu("TransitionToWalkState")]
    public void ToWalkState()
    {
        SwitchState(walkState);
    }
}
