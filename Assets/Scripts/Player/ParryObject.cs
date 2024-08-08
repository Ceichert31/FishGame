using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryObject : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    private void OnTriggerEnter(Collider other)
    {
        //Checks if the other object has a tag of parryable and if it does, call it's carry function, will convert this to an interface in the future
        if (other.CompareTag("Parryable"))
        {
            if(other.gameObject.TryGetComponent<ProjectileBehavior>(out ProjectileBehavior projectileBehavior))
            {
                projectileBehavior.Parried(playerCamera.forward);
            }
        }
    }
}
