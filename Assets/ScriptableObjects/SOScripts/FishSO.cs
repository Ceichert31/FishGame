using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Fish")]
public class FishSO : ScriptableObject
{
    [Tooltip("Fish encounter chance when catching a fish")]
    public RangedFloat spawnChance;

    [Tooltip("Fish's prefab")]
    public GameObject fishPrefab;
}
[Serializable]
public struct RangedFloat
{
    public float minValue;
    public float maxValue;
}
