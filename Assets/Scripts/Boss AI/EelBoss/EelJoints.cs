using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelJoints : MonoBehaviour
{
    [SerializeField] Transform bone;
    Transform follower;
    [SerializeField] float maxDistance;
    [SerializeField] bool last;

    public Transform Follower
    {
        get { return follower; }
        set { follower = value; }
    }

    private void Awake()
    {
        if(last)
        {
            follower = transform;
            maxDistance = 100;
        }
    }

    private void Update()
    {
        if(TooFar())
        {
            follower.position = NewPoint();
        }
        bone.position = transform.position;
    }

    public void SetMaxDistance()
    {
        maxDistance = Vector3.Distance(follower.position, transform.position) + .01f;
    }


    bool TooFar()
    {
        return Vector3.Distance(follower.position, transform.position) > maxDistance;
    }

    Vector3 NewPoint()
    {
        Vector3 direction = (follower.position - transform.position).normalized;
        return transform.position + direction * maxDistance;
    }
}
