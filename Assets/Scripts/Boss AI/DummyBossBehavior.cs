using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBossBehavior : MonoBehaviour
{
    private IProjectileSpawner projectileSpawner;

    // Start is called before the first frame update
    void Start()
    {
        projectileSpawner = GetComponent<IProjectileSpawner>();

        InvokeRepeating(nameof(Fire), 1f, 2f);
    }

    void Fire()
    {
        projectileSpawner.Spawn(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
