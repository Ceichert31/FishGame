using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lighting/Day Night")]
public class LightingPreset : ScriptableObject
{
    [SerializeField] private Gradient ambientColor;
    [SerializeField] private Gradient directionalColor;
    [SerializeField] private Gradient fogColor;
}
