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
        //Checks if the other object has a tag of parryable and if it does, call it's parry function

        if(!other.CompareTag("Parryable"))
        {
            return;
        }

        //Checks if the other object impliments the parry interface, and execture the parry behavior for said object if it does
        if (other.gameObject.TryGetComponent(out IParryable parriedObject))
        {
            parriedObject.OnParry();
            switch(parriedObject.ParryType)
            {
                case (int)ParryTypes.ProjectileParry:
                    parrySequencer.InitializeSequence();
                    break;
                case (int)ParryTypes.MeleeParry:
                    //IMeele attackBox = (IMeele)parriedObject;

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
/// Container for anything the a parryable object requries
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
