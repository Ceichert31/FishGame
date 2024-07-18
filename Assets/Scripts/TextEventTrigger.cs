using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private TextEventChannel text_EventChannel;

    [Header("Event Settings")]
    [SerializeField] private string textPrompt;

    [SerializeField] private float textClearTime = 2f;

    [SerializeField] private bool canClearText = true;

    [SerializeField] private bool isOneTimeUse = true;

    [SerializeField] private int triggerLayer;

    private TextEvent textEvent;

    void Start()
    {
        textEvent = new TextEvent(textPrompt, textClearTime, canClearText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == triggerLayer)
        {
            text_EventChannel.CallEvent(textEvent);

            if (isOneTimeUse)
            {
                gameObject.SetActive(false);
            }
        }
            
    }
}
