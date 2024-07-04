using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStates
{
    Idle,
    Recovering,
    Attacking,
    Pursuing,
    Parried
}

public class BossAIController : MonoBehaviour
{
    //Boss Specific Values
    BossStates currentState;

    [Header("Assignable Per Boss")]
    [SerializeField] Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case BossStates.Idle:
                break;
            case BossStates.Recovering:
                break;
            case BossStates.Attacking:
                break;
            case BossStates.Pursuing:
                break;
            case BossStates.Parried:
                break;
        }
    }

    void Idle()
    {

    }

    void Recovering()
    {
        
    }

    void Attacking()
    {

    }
    
    void Pursuing()
    {

    }
    
    void Parried()
    {

    }
}
