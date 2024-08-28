using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PustuleController : MonoBehaviour
{

    [SerializeField] GameObject pustule;
    float disableTime = 2f;

    public void OnHit(FloatEvent ctx)
    {
        Invoke(nameof(ReEnable), disableTime);
        pustule.SetActive(false);
    }

    void ReEnable()
    {
        pustule.SetActive(true);
    }
}
