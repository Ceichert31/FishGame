using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberInstance : MonoBehaviour
{
    [Header("Bobber Settings")]
    [SerializeField] private int waterLayer = 4;
    [SerializeField] bool inWater;
    float frequency = 1;
    float amplitude = .1f;
    Vector3 startPosition;

    //resets necisary variables on enable
    private void OnEnable()
    {
        startPosition = Vector3.zero;
        inWater = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == waterLayer)
        {
            if (collision.gameObject.TryGetComponent(out SpawnPool waterInstance))
            {
                waterInstance.StartFishing();

                //Sets value of start position to be able to procedurally move bobber
                startPosition = transform.position;
                inWater = true;
            }
        }
    }

    private void Update()
    {
        //Only move the bober if in water
        if (!inWater)
        {
            return;
        }

        //Create a new Y to be able to track our sin wave via the current time, a given frequncy, and amplitude
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        //Add that new Y to our position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
