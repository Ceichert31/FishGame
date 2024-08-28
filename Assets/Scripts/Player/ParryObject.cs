using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryObject : MonoBehaviour
{
    private Sequencer parrySequencer;
    [SerializeField] FloatEventChannel physicalParryEventChannel;

    [SerializeField] float physicalParryAmmount;

    FloatEvent parryAmmount;

    private void Start()
    {
        parrySequencer = GetComponent<Sequencer>();
        parryAmmount.FloatValue = physicalParryAmmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the other object has a tag of parryable and if it does, call it's carry function, will convert this to an interface in the future
        if (other.CompareTag("Parryable"))
        {
            if(other.gameObject.TryGetComponent(out IProjectile projectileBehavior))
            {
                parrySequencer.InitializeSequence();

                projectileBehavior.Parried(Camera.main.transform.forward);
            }
            else
            {
                other.gameObject.SetActive(false);
                physicalParryEventChannel.CallEvent(parryAmmount);
            }
        }
    }
}
