using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMeele
{
    bool Parried
    {
        get;
        set;
    }

    int ParryAmmount
    {
        get;
        set;
    }

    public void OnParry();
}

public class PhysicalParryBox : MonoBehaviour, IMeele
{
    [SerializeField] int parryAmmount;
    bool parried;

    Sequencer parrySequencer;

    public bool Parried { get => parried; set => parried = value; }
    public int ParryAmmount { get => parryAmmount; set => parryAmmount = value; }

    private void Start()
    {
        parrySequencer = transform.parent.GetComponent<Sequencer>();
    }

    public void OnParry()
    {
        if (parried)
        {
            return;
        }

        parrySequencer.InitializeSequence();

        parried = true;
        gameObject.SetActive(false);
        Invoke(nameof(ReEnable), .5f);
    }

    void ReEnable()
    {
        parried = false;
    }
        
}
