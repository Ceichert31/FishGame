using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBobber : MonoBehaviour
{
    BobberInstance bobberInstance;

    float frequency = 1;

    float amplitude = .06f;

    Vector3 startPosition => bobberInstance.transform.position;

    private void Awake()
    {

        try
        {
            bobberInstance = GetComponentInParent<BobberInstance>();
        }
        catch
        {
            throw new System.Exception("No bobber instance found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Only move the bober if in water
        if (!bobberInstance.InWater)
        {
            return;
        }

        //Create a new Y to be able to track our sin wave via the current time, a given frequncy, and amplitude
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        //Add that new Y to our position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
