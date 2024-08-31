using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeController : MonoBehaviour
{
    private Volume currentProfile;

    private bool isNull = true;

    private PostEvent cachedProfile;

    void Awake()
    {
        isNull = !TryGetComponent(out currentProfile);     
    }

    /// <summary>
    /// Change out current volume profile
    /// </summary>
    /// <param name="ctx"></param>
    public void SetProfile(PostEvent ctx)
    {
        if (isNull) return;

        cachedProfile.Profile = currentProfile.profile;

        currentProfile.profile = ctx.Profile;
    }
    
    /// <summary>
    /// Resets profile to cached profile
    /// </summary>
    /// <param name="ctx"></param>
    public void ResetProfile(VoidEvent ctx)
    {
        currentProfile.profile = cachedProfile.Profile;
    }
}
