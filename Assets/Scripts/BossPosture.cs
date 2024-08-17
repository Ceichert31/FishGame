using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPosture : MonoBehaviour
{
    [SerializeField] FloatEventChannel maxPosture_EventChannel;
    [SerializeField] FloatEventChannel posture_EventChannel;
    [SerializeField] VoidEventChannel stagger_EventChannel;
    //EXPERIMENTAL
    [SerializeField] FloatEventChannel staggerTimer_EventChannel;

    [SerializeField] float maxPosture = 100;

    private FloatEvent currentPosture;
    private FloatEvent staggerTime;

    // Start is called before the first frame update
    void Awake()
    {
        //Sets the current posture float event to the maxPosture at the start for UI purposes
        currentPosture.FloatValue = maxPosture;

        //Calls the method on the maxPosture float event listener to update the posture bar to be equal to the max posture
        maxPosture_EventChannel.CallEvent(currentPosture);
    }

    // Update is called once per frame
    public void UpdatePosture(FloatEvent ctx)
    {
        //subtracts the posture from our currentPosture FloatEvent
        currentPosture.FloatValue -= ctx.FloatValue;

        //Calls the method on the Float Event Listener for the posture to be updated for the UI
        posture_EventChannel.CallEvent(currentPosture);

        //If the posture is broken, call the method on the staggerTimer event channel, and start the timer
        if(currentPosture.FloatValue < 0)
        {
            staggerTimer_EventChannel.CallEvent(new());
            StartCoroutine(StaggerTime());
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
        float timer = 10;
        staggerTime.FloatValue = timer;
        while(true)
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
        currentPosture.FloatValue = maxPosture;
        posture_EventChannel.CallEvent(currentPosture);
    }

    //Future method used to stop the stagger timer in case of boss getting hit before the timer ends(not implimented yet)
    public void StopStaggerTimer()
    {
        StopAllCoroutines();
        ResetPosture();
    }
}
