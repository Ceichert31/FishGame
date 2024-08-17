using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPosture : MonoBehaviour
{
    [SerializeField] FloatEventChannel maxPosture_EventChannel;
    [SerializeField] FloatEventChannel posture_EventChannel;
    [SerializeField] VoidEventChannel stagger_EventChannel;
    //EXPERIMENTAL
    [SerializeField] FloatEventChannel staggerTimer_EventChannel;

    [SerializeField] float maxPosture = 100;

    private float playerDamage => GameManager.Instance.PlayerDamage;

    private FloatEvent currentPosture;
    private FloatEvent staggerTime;

    // Start is called before the first frame update
    void Start()
    {
        currentPosture.FloatValue = 0f;

        //Calls the method on the maxPosture float event listener to update the posture bar to be equal to the max posture
        maxPosture_EventChannel.CallEvent(new(maxPosture));
    }

    // Update is called once per frame
    public void UpdatePosture(float damage)
    {
        //Adds the posture from our currentPosture FloatEvent
        currentPosture.FloatValue += damage;

        //Calls the method on the Float Event Listener for the posture to be updated for the UI
        posture_EventChannel.CallEvent(currentPosture);

        //If the posture is broken, call the method on the staggerTimer event channel, and start the timer
        if (currentPosture.FloatValue >= maxPosture)
        {
            staggerTimer_EventChannel.CallEvent(new());

            StartCoroutine(StaggerTime());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If collision object is parried projectile, deal posture damage
        if (other.TryGetComponent(out IProjectile projectileInstance))
        {
            if (projectileInstance.IsParried)
            {
                UpdatePosture(playerDamage);

                projectileInstance.DisableProjectile();

                //Get collsion point and play special effects
            }
        }
    }

    /// <summary>
    /// Coroutine that is a timer
    /// This Timer is used by the stagger state in order to determine if the boss is still stagered
    /// If the timmer is up it will break out of the this coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator StaggerTime()
    {
        float timer = 10f;

        staggerTime.FloatValue = timer;

        while (true)
        {
            timer -= Time.deltaTime;

            staggerTime.FloatValue = timer;

            staggerTimer_EventChannel.CallEvent(staggerTime);

            if (timer < 0)
            {
                break;
            }
            yield return null;
        }
        ResetPosture();
    }

    //Resets Posture Values
    void ResetPosture()
    {
        currentPosture.FloatValue = 0f;

        posture_EventChannel.CallEvent(currentPosture);
    }

    //Future method used to stop the stagger timer in case of boss getting hit before the timer ends(not implimented yet)
    public void StopStaggerTimer()
    {
        StopAllCoroutines();
        ResetPosture();
    }
}
