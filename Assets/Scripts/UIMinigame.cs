using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIMinigame : MonoBehaviour
{
    [Header("Grapple Bar Referenes")]
    [SerializeField] private Image pointer;

    [Header("Grapple Bar Settings")]
    [SerializeField] private float scrollSpeed = 3f;

    [SerializeField] private Transform endPos;

    private bool hasClicked;

    /// <summary>
    /// Starts attack minigame
    /// </summary>
    /// <param name="ctx"></param>
    public void StartMinigame(FloatEvent ctx)
    {
        pointer.transform.localPosition = Vector3.zero;

        StartCoroutine(AttackMinigame());
    }
    IEnumerator AttackMinigame()
    {
        RangedFloat rangedFloat;
        rangedFloat.minValue = -150f;
        rangedFloat.maxValue = -100f;

        int finalDamage = 0;

        while (Vector2.Distance(pointer.transform.position, endPos.position) > 0.1f)
        {
            pointer.transform.position = Vector2.MoveTowards(pointer.transform.position, endPos.position, scrollSpeed * Time.unscaledDeltaTime);

            //Click signal
            if (hasClicked)
            {
                hasClicked = false;

                //Check if click is valid
                if (pointer.transform.position.x >= rangedFloat.minValue && pointer.transform.position.x <= rangedFloat.maxValue)
                {
                    finalDamage++;
                }
                else
                {
                    Debug.Log("MISSED!!!");
                }
            }

            yield return null;
        }

        pointer.transform.position = endPos.position;

        //Debug.Log(finalDamage);
    }

    public void PlayerClick(InputAction.CallbackContext ctx)
    {
        hasClicked = true;
    }

    public void InitializeControls(InputEvent ctx)
    {
        ctx.Action.ReelIn.Click.performed += PlayerClick;
    }
}
