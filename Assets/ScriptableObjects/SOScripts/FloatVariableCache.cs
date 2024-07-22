using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Caches/Float Variable Cache")]
public class FloatVariableCache : GenericVariableCache<float>
{
    //Clamped setter
    public override float CachedValue 
    { 
        get => base.CachedValue; 
        set => base.CachedValue = Mathf.Clamp(value, 0, 20); 
    }
}
