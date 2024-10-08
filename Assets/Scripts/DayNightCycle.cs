using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class DayNightCycle : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private BoolEventChannel time_EventChannel;
    [SerializeField] private PostEffectEventChannel postEffect_EventChannel;
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


    [Header("Day/Night Profiles")]
    [SerializeField] private PostEvent dayProfile;

    [SerializeField] private PostEvent nightProfile;

    private Coroutine instance;

    private BoolEvent isDayTime;

    private float advanceTimeTimer;

    private Sequencer advanceTimeSequencer;

    //Consts
    private const float TOTALROTATION = 360f;

    private const float TOTALDAYTIME = 24f;

    private const float SUNRISE = 7f;

    private const float SUNSET = 18f;

    private const float ADVANCETIMEDELAY = 0.5f;

    private void Awake()
    {
        mainLight = transform.GetChild(0).GetComponent<Light>();

        ambientLight = transform.GetChild(0).GetChild(0).GetComponent<Light>();

        timeOfDay = defaultTimeOfDay;

        advanceTimeSequencer = GetComponent<Sequencer>();
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
            SetDayTimeVisuals();
        }
        else
        {
            SetNightTimeVisuals();
        }
    }

    private void OnValidate()
    {
        UpdateLighting(timeOfDay / TOTALDAYTIME);
    }

    /// <summary>
    /// Runs everything related to daytime functionallity
    /// </summary>
    private void SetDayTimeVisuals()
    {
        instance = StartCoroutine(SetAmbientLightIntensity(1f));

        isDayTime.Value = true;
        time_EventChannel.CallEvent(isDayTime);

        postEffect_EventChannel.CallEvent(dayProfile);
    }

    /// <summary>
    /// Runs everything related to nighttime functionallity
    /// </summary>
    private void SetNightTimeVisuals()
    {
        instance = StartCoroutine(SetAmbientLightIntensity(1.5f));

        isDayTime.Value = false;
        time_EventChannel.CallEvent(isDayTime);

        postEffect_EventChannel.CallEvent(nightProfile);
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

    public void StartTimeTransition(VoidEvent ctx)
    {
        if (advanceTimeTimer > Time.time) return;

        advanceTimeTimer = Time.time + ADVANCETIMEDELAY;

        advanceTimeSequencer.InitializeSequence();
    }

    /// <summary>
    /// Advances time by a certain amount
    /// </summary>
    /// <param name="ctx"></param>
    public void AdvanceTime(VoidEvent ctx)
    {
        timeOfDay += advanceTimeBy;
    }

    /// <summary>
    /// Sets the density of unity fog
    /// </summary>
    /// <param name="ctx"></param>
    public void SetFogDensity(FloatEvent ctx)
    {
        RenderSettings.fogDensity = ctx.FloatValue;
    }
}
