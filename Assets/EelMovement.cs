using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelMovement : MonoBehaviour
{
    [SerializeField] float amplitude = .0005f;
    [SerializeField] float frequency = 1f;

    [SerializeField] bool pos = true;
    // Update is called once per frame
    void Update()
    {
        if(!pos)
        {
            transform.position += new Vector3(Mathf.Sin(frequency * Time.time) * amplitude, 0, 0);
            return;
        }
        transform.position += new Vector3(Mathf.Cos(frequency * Time.time) * amplitude, 0, 0);
    }
}
