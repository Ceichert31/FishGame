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

    private Sequencer attackSequencer;

    private AudioSource source;

    private void Start()
    {
        attackSequencer = GetComponent<Sequencer>();

        source = transform.parent.GetComponent<AudioSource>();

        //Disabled after game starts
        gameObject.SetActive(false);
    }
    public void FireHarpoon()
    {
        Quaternion projectileRotation = Quaternion.LookRotation(Camera.main.transform.forward);

        GameObject instance = Instantiate(harpoonProjectile, transform.position, Quaternion.identity);

        instance.transform.up = firingPoint.forward;

        HarpoonProjectile projectile = instance.GetComponent<HarpoonProjectile>();
        projectile.Init(projectileDamage, projectileSpeed, projectileLifetime, Camera.main.transform.forward);
    }
   
}

