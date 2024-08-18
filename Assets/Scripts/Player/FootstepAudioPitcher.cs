using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioPitcher : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] AudioPitcherSO groundFootstep_Pitcher;
    [SerializeField] AudioPitcherSO waterFootstep_Pitcher;
    [SerializeField] AudioPitcherSO woodFootstep_Pitcher;

    [SerializeField] private float timeBetweenSteps = 0.3f;

    [SerializeField] private float audioStartDelay = 1f;

    [SerializeField] private float rayCastDistance = 2f;

    private InputController inputController;

    private float currentDelayTime;

    private float currentTime;

    private AudioSource source;

    private RaycastHit materialHit;

    private AudioSO currentAudioPitcher;

    private string currentLayer;

    private void Start()
    {
        inputController = GetComponent<InputController>();

        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Return if playing SFX is not applicable 
        if (!inputController.IsMoving || !inputController.IsGrounded) 
        {
            currentDelayTime = 0;
            return;
        }

        //Fire raycast to get layer underneath player
        if (Physics.Raycast(transform.position, -Vector3.up, out materialHit, rayCastDistance))
            currentLayer = materialHit.transform.gameObject.tag;

        //Switch between audio pitchers based on material
        switch (currentLayer)
        {
            default:
                currentAudioPitcher = groundFootstep_Pitcher;
                break;

            case "Water":
                currentAudioPitcher = waterFootstep_Pitcher;
                break;

            case "Wood":
                currentAudioPitcher = woodFootstep_Pitcher;
                break;
        }

        PlayFootsteps();
    }

    private void PlayFootsteps()
    {
        if (inputController.IsMoving)
        {
            if (currentTime < Time.time)
            {
                currentTime = Time.time + timeBetweenSteps;

                currentAudioPitcher.Play(source);
            }
        }
    }
}
