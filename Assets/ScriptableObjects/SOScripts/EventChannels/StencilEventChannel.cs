using UnityEngine;
[CreateAssetMenu(menuName = "Events/Stencil Event Channel")]
public class StencilEventChannel : GenericEventChannel<StencilEvent> {}

[System.Serializable]
public struct StencilEvent
{
    public int StencilID;
    public bool IsEnabled;
    public StencilEvent(int stencilID, bool isEnabled)
    {
        StencilID = stencilID;
        IsEnabled = isEnabled;
    }
}