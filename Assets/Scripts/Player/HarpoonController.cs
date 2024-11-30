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
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 6f;

    [SerializeField] private Transform firingPoint;

    private Animator harpoonAnimator;

    private Sequencer attackSequencer;

    private AudioSource source;

    private GameObject harpoon;

    private bool canFire = true;

    private bool isReloading;
    
    //Getter
    public bool _CanFire => canFire;

    public bool _Reloading => isReloading;

    private void Start()
    {
        harpoonAnimator = GetComponent<Animator>();

        attackSequencer = GetComponent<Sequencer>();

        source = transform.parent.GetComponent<AudioSource>();
        
        harpoon = transform.GetChild(0).gameObject;

        //Disabled after game starts
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Plays harpoon fire animation and fires projectile
    /// </summary>
    public void FireHarpoon()
    {
        //Prevents firing before reload
        if (!canFire) return;

        canFire = false;

        //Trigger animation event
        harpoonAnimator.SetTrigger("Fire");

        //Play attack sequencer
        attackSequencer.InitializeSequence();

        //Instantiate projectile 
        GameObject instance = Instantiate(harpoonProjectile, transform.position, Quaternion.identity);
        instance.transform.up = firingPoint.forward;

        //Get projectile class and initialize it
        HarpoonProjectile projectile = instance.GetComponent<HarpoonProjectile>();
        projectile.Init(projectileSpeed, projectileLifetime, Camera.main.transform.forward);
    }
    /// <summary>
    /// Plays harpoon reload animation
    /// </summary>
    public void Reload()
    {
        if (canFire) return;
        harpoonAnimator.SetTrigger("Reload");
        isReloading = true;
    }
    /// <summary>
    /// Called by animator
    /// </summary>
    public void CanFire()
    {
        canFire = true;
        isReloading = false;
    }
    /// <summary>
    /// Called by animator
    /// </summary>
    public void SetHarpoon()
    {
        harpoonAnimator.SetBool("canFire", canFire);
    }
    /// <summary>
    /// Called by animator
    /// </summary>
    public void HarpoonEnable()
    {
        harpoon.SetActive(true);
    }
    /// <summary>
    /// Called by animator
    /// </summary>
    public void HarpoonDisable()
    {
        harpoon.SetActive(false);
    }
}

