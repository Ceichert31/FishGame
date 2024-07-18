using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Hooked fish Event Channel")]
public class HookedFishEventChannel : GenericEventChannel<HookedEvent> {}
[System.Serializable]
public struct HookedEvent
{
    public FishSO fishInstance;

    public HookedEvent(FishSO instance) => fishInstance = instance;
}
