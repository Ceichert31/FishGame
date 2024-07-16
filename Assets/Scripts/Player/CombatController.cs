using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    [Header("Hook Settings")]
    [SerializeField] private float parryDelay = 0.8f;

    private float parryCooldown;

    private Animator hookAnimator;

    private void Awake()
    {
        hookAnimator = transform.GetChild(1).GetComponent<Animator>();
    }

    void FireGrapple(InputAction.CallbackContext ctx)
    {
        
    }

    void Parry(InputAction.CallbackContext ctx) 
    {
        //Time gate to prevent queued parries
        if (parryCooldown <= Time.time)
        {
            parryCooldown = Time.time + parryDelay;

            hookAnimator.SetTrigger("Parry");
        }
    }   

    /// <summary>
    /// Subscribes functions to the correct controls
    /// </summary>
    /// <param name="ctx"></param>
    public void InitializeControls(InputEvent ctx)
    {
        ctx.Action.Combat.Disable();

        ctx.Action.Combat.ReelIn.performed += Parry;

        ctx.Action.Combat.Fire.performed += FireGrapple;
    }
}
