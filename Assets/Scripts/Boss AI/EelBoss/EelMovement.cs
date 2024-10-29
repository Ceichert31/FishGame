using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelMovement : MonoBehaviour
{
    private void Awake()
    {
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            EelJoints segment = transform.GetChild(i).GetComponent<EelJoints>();
            segment.Follower = transform.GetChild(i + 1);
            segment.SetMaxDistance();
        }
    }
}
