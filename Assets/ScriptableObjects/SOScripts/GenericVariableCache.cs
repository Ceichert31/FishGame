using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GenericVariableCache<T> : ScriptableObject
{
    private T cachedValue;

    public virtual T CachedValue 
    { 
        get { return cachedValue; } 
        set { cachedValue = value; }
    }
}
