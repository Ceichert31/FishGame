using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[ExecuteInEditMode]
public class DayNightCycle : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private LightingPreset preset;

    [Range(0f, 24f)]
    [SerializeField] private float timeOfDay;

    private DirectionalLight mainLight;

    private void OnValidate()
    {
        
    }
}
