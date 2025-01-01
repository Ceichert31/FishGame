using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHandler : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void PixelFade(BoolEvent fadeIn)
    {
        if (fadeIn.Value)
            animator.SetTrigger("In");
        else
            animator.SetTrigger("Out");
    }
}
