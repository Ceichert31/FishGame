using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lighting/Day Night")]
public class LightingPreset : ScriptableObject
{
    public Gradient directionalColor;
    public Gradient moonLightColor;
    public Gradient fogColor;
    [GradientUsage(true)]
    public Gradient ambientLight;
}
