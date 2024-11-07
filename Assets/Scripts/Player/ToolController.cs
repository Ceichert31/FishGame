using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    private GameObject fishingRod;

    private GameObject harpoonGun;

    private Animator fishRodAnimator;

    private GameObject stencilRenderer;

    private void Start()
    {
        fishingRod = transform.GetChild(0).gameObject;

        harpoonGun = transform.GetChild(2).gameObject;

        fishRodAnimator = transform.GetChild(0).GetComponent<Animator>();

        stencilRenderer = transform.GetChild(4).gameObject;
    }

    public void EnableFishingPoleModel(VoidEvent ctx)
    {
        fishingRod.SetActive(true);

        fishRodAnimator.SetTrigger("Pickup");
    }

    public void SwitchTools(BoolEvent ctx)
    {
        //Combat mode
        if (ctx.Value)
        {
            fishingRod.SetActive(false);
            harpoonGun.SetActive(true);
            stencilRenderer.SetActive(true);
        }
        //Fishing mode
        else 
        {
            fishingRod.SetActive(true);
            harpoonGun.SetActive(false);
            stencilRenderer.SetActive(false);
        } 
            
    }
}
