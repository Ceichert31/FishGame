using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private TextEventChannel text_EventChannel;

    [Header("Text Event Settings")]
    [SerializeField] private List<TextEvent> textList;

    [Tooltip("Whether or not the trigger will be disabled after being interacted with")]
    [SerializeField] private bool isOneTimeUse = true;

    private const int PLAYERLAYER = 6;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PLAYERLAYER)
        {
            foreach(TextEvent textInstance in textList)
                text_EventChannel.CallEvent(textInstance);

            //Disable after interaction
            if (isOneTimeUse)
                gameObject.SetActive(false);
        }
    }
}
