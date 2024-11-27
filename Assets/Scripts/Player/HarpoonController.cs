using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperMethods;
using Unity.VisualScripting;

public class HarpoonController : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private VoidEventChannel fov_EventChannel;

    [Header("Projectile Settings")]
    [SerializeField] private GameObject harpoonProjectile;
    [SerializeField] private float projectileDamage = 5f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 6f;

    [SerializeField] private Transform firingPoint;

    private Animator harpoonAnimator;

    private Sequencer attackSequencer;

    private AudioSource source;

    private bool canFire = true;

    private GameObject harpoon;
    
    //Getter
    public bool _CanFire => canFire;

    private void Start()
    {
        harpoonAnimator = GetComponent<Animator>();

        attackSequencer = GetComponent<Sequencer>();

        source = transform.parent.GetComponent<AudioSource>();
        
        harpoon = transform.GetChild(0).gameObject;

        //Disabled after game starts
        gameObject.SetActive(false);
    }
    public void FireHarpoon()
    {
        if (!canFire) return;

        harpoonAnimator.SetTrigger("Fire");

        canFire = false;

        GameObject instance = Instantiate(harpoonProjectile, transform.position, Quaternion.identity);
        instance.transform.up = firingPoint.forward;

        HarpoonProjectile projectile = instance.GetComponent<HarpoonProjectile>();
        projectile.Init(projectileDamage, projectileSpeed, projectileLifetime, Camera.main.transform.forward);

        harpoonAnimator.SetBool("canFire", canFire);
    }

    private void Update()
    {
        Debug.Log(canFire);
    }
    public void Reload()
    {
        harpoonAnimator.SetTrigger("Reload");
    }
    public void CanFire()
    {
        canFire = true;
        harpoonAnimator.SetBool("canFire", canFire);
    }
    public void SetHarpoon()
    {
        harpoon.SetActive(false);
    }
}

