using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Events/Post Event Channel")]

public class PostEffectEventChannel : GenericEventChannel<PostEvent> {}

[System.Serializable]
public struct PostEvent
{
    public VolumeProfile Profile;

    public PostEvent(VolumeProfile profile) => Profile = profile;
}