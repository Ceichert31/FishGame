using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DayNightCycle : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private LightingPreset preset;

    [Header("Day Night Settings")]
    [Range(0f, 24f)]
    [SerializeField] private float timeOfDay;

    [SerializeField] private float timeSpeedMultiplier = 0.5f;

    [SerializeField] private float defaultTimeofDay = 4f;

    [SerializeField] private Vector3 lightRotation;

    private Light mainLight;

    //Consts
    private const float TOTALROTATION = 360f;

    private const float TOTALDAYTIME = 24f;

    private void Awake()
    {
        mainLight = transform.GetChild(0).GetComponent<Light>();

        timeOfDay = defaultTimeofDay;
    }

    private void Update()
    {
        if (preset == null) return;

        if (Application.isPlaying)
        {
            //Add time
            timeOfDay += Time.deltaTime * timeSpeedMultiplier;

            //Clamp
            timeOfDay %= TOTALDAYTIME;

            UpdateLighting(timeOfDay / TOTALDAYTIME);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        if (mainLight != null ) 
        {
            mainLight.color = preset.directionalColor.Evaluate(timePercent);

            mainLight.transform.localRotation = Quaternion.Euler(new((timePercent * TOTALROTATION) - lightRotation.x, lightRotation.y, lightRotation.z));
        }
    }
}
