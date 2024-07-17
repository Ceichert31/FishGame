using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberInstance : MonoBehaviour
{
    [Header("Bobber Settings")]
    [SerializeField] private int waterLayer = 4;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == waterLayer)
        {
            if (collision.gameObject.TryGetComponent(out SpawnPool waterInstance))
            {
                waterInstance.StartFishing();
            }
        }
    }
}
