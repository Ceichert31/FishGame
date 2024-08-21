using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryBoxBehavior : MonoBehaviour
{
    [SerializeField] GameObject parryBox;

    /// <summary>
    /// Activates and deactives the parry box via animation events
    /// </summary>
    public void ActivateParryBox()
    {
        parryBox.SetActive(true);
    }

    public void DeactivateParryBox()
    {
        parryBox.SetActive(false);
    }
}
