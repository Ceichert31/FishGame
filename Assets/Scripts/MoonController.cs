using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    [SerializeField] private float distance = 1000f;
    [SerializeField] private Vector3 scale;

    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, distance);
        transform.localScale = scale;
    }
}
