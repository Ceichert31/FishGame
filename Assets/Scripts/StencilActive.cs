using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilActive : MonoBehaviour
{
    [SerializeField] private int stencilID;

    private GameObject childObject;

    private void Start()
    {
        childObject = gameObject.transform.GetChild(0).gameObject;
        childObject.SetActive(false);
    }

    public void UpdateObjectStatus(StencilEvent ctx)
    {
        if (stencilID != ctx.StencilID) return;

        childObject.SetActive(ctx.IsEnabled);
    }
}
