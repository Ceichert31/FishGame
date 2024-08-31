using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMeele
{
    bool Used
    {
        get;
        set;
    }

    int ParryAmmount
    {
        get;
        set;
    }

    int AttackDamage
    {
        get;
        set;
    }

    public void OnParry();
}

public class PhysicalParryBox : MonoBehaviour, IMeele
{
    [SerializeField] int parryAmmount;
    [SerializeField] int attackDamage;
    bool used;

    Sequencer parrySequencer;

    public bool Used { get => used; set => used = value; }
    public int ParryAmmount
    {
        get {
            OnParry();
            return parryAmmount; 
            }
      set => parryAmmount = value; 
    }
    public int AttackDamage 
    { 
        get {
            OnDealtDamage();
            return attackDamage; 
            } 
        set => attackDamage = value; 
    }

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
        parrySequencer.InitializeSequence();
        OnAction();
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
