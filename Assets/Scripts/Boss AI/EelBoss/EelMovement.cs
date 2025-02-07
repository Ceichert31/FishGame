using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelMovement : MonoBehaviour
{
    [SerializeField] Transform[] eelBones;
    [SerializeField] Vector3[] previousPositions = new Vector3[15];
    [SerializeField] Transform ORB;

    [SerializeField] float boneLength;
    [SerializeField] float followTime;

    private void Start()
    {
        for (int i = 1; i < eelBones.Length; i++)
        {
            previousPositions[i] = eelBones[i].position;
        }
    }

    private void Update()
    {
        UnParent();
    }

    //Nesisary Unparent to get desired behavior
    void UnParent()
    {
        for(int i = 1; i < eelBones.Length; i++)
        {
            eelBones[i].position = previousPositions[i];


            //NormalSnakeLike Behavior
            ChainSlither(i);

            //ReAllignmentBehavior
            //AllignmentSlither(i);


            previousPositions[i] = eelBones[i].position;
        }
    }

    //Chain Slither for normal snake like movement
    void ChainSlither(int i)
    {
        if (Vector3.Distance(eelBones[i].position, eelBones[i - 1].position) > boneLength)
        {
            eelBones[i].position = Vector3.Lerp(eelBones[i].position, eelBones[i - 1].position, Time.deltaTime * followTime);

            AllignRotations(i);
        }
    }

    //AllignmentSlither for alligning the body
    void AllignmentSlither(int i)
    {
        Vector3 targetPosition = eelBones[i - 1].position - ORB.forward * boneLength;
        eelBones[i].position = Vector3.Lerp(eelBones[i].position, targetPosition, Time.deltaTime * followTime);

        AllignRotations(i);
    }

    void AllignRotations(int i)
    {
        //Allign Rotations
        Vector3 boneDirection = (eelBones[i].position - eelBones[i - 1].position).normalized;

        Quaternion targetDirection = Quaternion.LookRotation(eelBones[i].forward, boneDirection);
        eelBones[i].rotation = Quaternion.Slerp(eelBones[i].rotation, targetDirection, Time.deltaTime * followTime);
    }
}
