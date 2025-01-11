using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelMovement : MonoBehaviour
{
    [SerializeField] Transform[] eelBones;
    private void Awake()
    {

    }

    [ContextMenu("ReAllign")]
    public void ReAllign()
    {
        for(int i = 1; i < eelBones.Length; i++)
        {
            eelBones[i].position = eelBones[i - 1].position;
        }
    }
}
