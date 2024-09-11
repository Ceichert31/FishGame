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

        /*if (other.gameObject.TryGetComponent(out IProjectile projectileBehavior))
        {
            parrySequencer.InitializeSequence();

            projectileBehavior.Parried(Camera.main.transform.forward);
            return;
        }*/

        /*if (other.gameObject.TryGetComponent(out IMeele attackBox))
        {
            if (attackBox.Used)
            {
                return;
            }
            parryAmmount.FloatValue = attackBox.ParryAmmount;
            
            physicalParryEventChannel.CallEvent(parryAmmount);

            resetParry_EventChannel.CallEvent(voidEvent);
        }*/

        if (other.gameObject.TryGetComponent(out IParryable parriedObject))
        {
            parriedObject.OnParry();
            switch(parriedObject.ParryType)
            {
                case (int)ParryTypes.ProjectileParry:
                    parrySequencer.InitializeSequence();
                    break;
                case (int)ParryTypes.MeleeParry:
                    IMeele attackBox = (IMeele)parriedObject;

                    parryAmmount.FloatValue = parriedObject.ParryAmount;

                    physicalParryEventChannel.CallEvent(parryAmmount);

                    resetParry_EventChannel.CallEvent(voidEvent);
                    break;
            }
        }
    }
}

/// <summary>
/// Location: ParryObject
/// 
/// </summary>
public interface IParryable
{
    public int ParryType { get; }
    public void OnParry();
    public float ParryAmount {  get; }
    public bool Parried { get; }
}

public enum ParryTypes
{
    ProjectileParry,
    MeleeParry
}