using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMeele
{
    int AttackDamage
    {
        get;
        set;
    }
}

public class PhysicalParryBox : MonoBehaviour, IMeele, IParryable
{
    [SerializeField] int parryAmmount;
    [SerializeField] int attackDamage;
    bool used;

    Sequencer parrySequencer;
    public int AttackDamage 
    { 
        get {
            OnDealtDamage();
            return attackDamage; 
            } 
        set => attackDamage = value; 
    }

    public bool Parried => used;

    public int ParryType => (int)ParryTypes.MeleeParry;

    public float ParryAmount => parryAmmount;

    private void Start()
    {
        parrySequencer = transform.parent.GetComponent<Sequencer>();
    }

    public void OnParry()
    {
        if (used)
        {
            return;
        }

        OnAction();
        parrySequencer.InitializeSequence();
    }

    public void OnDealtDamage()
    {
        if (used)
        {
            return;
        }

        OnAction();
    }

    void OnAction()
    {
        used = true;
        gameObject.SetActive(false);
        Invoke(nameof(ReEnable), .5f);
    }

    void ReEnable()
    {
        used = false;
    }
        
}
