using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DayNightCycle : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private BoolEventChannel time_EventChannel;
    [SerializeField] private LightingPreset preset;

    [Header("Day Night Settings")]

    [SerializeField] private bool pauseTime;

    [Range(0f, 24f)]
    [SerializeField] private float timeOfDay;

    [SerializeField] private float timeSpeedMultiplier = 0.5f;

    [SerializeField] private float defaultTimeOfDay = 4f;

    [SerializeField] private float advanceTimeBy = 12f;

    [SerializeField] private Vector3 lightRotation;

    [SerializeField] private Light mainLight;

    [SerializeField] private Light ambientLight;

    private Coroutine instance;

    private BoolEvent isDayTime;

    private float advanceTimeTimer;

    //Consts
    private const float TOTALROTATION = 360f;

    private const float TOTALDAYTIME = 24f;

    private const float SUNRISE = 7f;

    private const float SUNSET = 18f;

    private const float ADVANCETIMEDELAY = 0.5f;

    private void Awake()
    {
        mainLight = transform.GetChild(0).GetComponent<Light>();

        ambientLight = transform.GetChild(1).GetComponent<Light>();

        timeOfDay = defaultTimeOfDay;
    }

    private void Update()
    {
        if (pauseTime) return;

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
        ambientLight.color = preset.ambientColor.Evaluate(timePercent);

        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        if (mainLight != null)
        {
            mainLight.color = preset.directionalColor.Evaluate(timePercent);

            mainLight.transform.localRotation = Quaternion.Euler(new((timePercent * TOTALROTATION) - lightRotation.x, lightRotation.y, lightRotation.z));
        }

        if (instance != null) return;

        //Intensity based on time of day
        if (timeOfDay > SUNRISE && timeOfDay < SUNSET)
        {
            instance = StartCoroutine(SetAmbientLightIntensity(1f));

            isDayTime.Value = true;
            time_EventChannel.CallEvent(isDayTime);
        }
        else
        {
            instance = StartCoroutine(SetAmbientLightIntensity(3f));

            isDayTime.Value = false;
            time_EventChannel.CallEvent(isDayTime);
        }
    }

    private void OnValidate()
    {
        UpdateLighting(timeOfDay / TOTALDAYTIME);
    }

    IEnumerator SetAmbientLightIntensity(float targetIntensity)
    {
        while (ambientLight.intensity != targetIntensity)
        {
            ambientLight.intensity = Mathf.MoveTowards(ambientLight.intensity, targetIntensity, Time.deltaTime);

            yield return null;
        }

        ambientLight.intensity = targetIntensity;

        instance = null;
    }

    /// <summary>
    /// Pauses and unpauses day/night cycle progression
    /// </summary>
    /// <param name="ctx"></param>
    public void PauseTime(BoolEvent ctx)
    {
        pauseTime = ctx.Value;
    }

    /// <summary>
    /// Advances time by a certain amount
    /// </summary>
    /// <param name="ctx"></param>
    public void AdvanceTime(VoidEvent ctx)
    {
        if (advanceTimeTimer > Time.time) return;

        advanceTimeTimer = Time.time + ADVANCETIMEDELAY;

        timeOfDay += advanceTimeBy;
    }
}
