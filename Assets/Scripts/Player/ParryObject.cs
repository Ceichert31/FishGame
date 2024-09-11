using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryObject : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private VoidEventChannel resetParry_EventChannel;

    private Sequencer parrySequencer;
    [SerializeField] FloatEventChannel physicalParryEventChannel;

    FloatEvent parryAmmount;

    VoidEvent voidEvent;

    private void Start()
    {
        parrySequencer = transform.parent.GetComponent<Sequencer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the other object has a tag of parryable and if it does, call it's carry function, will convert this to an interface in the future

        if(!other.CompareTag("Parryable"))
        {
            return;
        }

        if (other.gameObject.TryGetComponent(out IProjectile projectileBehavior))
        {
            parrySequencer.InitializeSequence();

            projectileBehavior.Parried(Camera.main.transform.forward);
            return;
        }

        if (other.gameObject.TryGetComponent(out IMeele attackBox))
        {
            if (attackBox.Used)
            {
                return;
            }
            parryAmmount.FloatValue = attackBox.ParryAmmount;
            
            physicalParryEventChannel.CallEvent(parryAmmount);

            resetParry_EventChannel.CallEvent(voidEvent);
        }

        /*if (other.gameObject.TryGetComponent(out IParryable parriedObject))
        {
            parriedObject.OnParry();
        }*/
    }
}

public interface IParryable
{
    public void OnParry();
    public bool Parried { get; }
}