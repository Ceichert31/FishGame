using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilController : MonoBehaviour
{
    [SerializeField] private List<GameObject> stencilRenderers = new();

    private void Start()
    {
        //Fill list with all stencil renderers
        for (int i = 0; i < transform.childCount; i++)
        {
            stencilRenderers.Add(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Can enable/disable stencils using their ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isEnabled"></param>
    public void UpdateStencil(int id, bool isEnabled)
    {
        //Bootstrap case
        if (id > stencilRenderers.Count) return;

        //Set stencil renderer status
        stencilRenderers[id - 1].SetActive(isEnabled);
    }
}