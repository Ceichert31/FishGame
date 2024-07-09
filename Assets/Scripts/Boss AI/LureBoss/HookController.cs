using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionTracker))]
public class HookController : MonoBehaviour
{
    [Header("Hook List")]
    [SerializeField] List<Transform> hooks = new List<Transform>();

    [Header("Hook Limits")]
    float maxYRot = 75;
    float maxXRot = 30;
    float rotateAmmount = 100f;
    float rotateBackAmmount = 10f;

    [SerializeField] float tempHookX;
    [SerializeField] float tempHookY;
    Vector3 tempDirection;

    DirectionTracker directionTracker;
    Vector3 rotateDirection;
    

    // Start is called before the first frame update
    void Awake()
    {
        directionTracker = GetComponent<DirectionTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateDirection = directionTracker.PlayerDirection;

        if (rotateDirection.magnitude == 0)
        {
            for (int i = 0; i < hooks.Count; i++)
            {
                hooks[i].localRotation = Quaternion.Lerp(hooks[i].localRotation, Quaternion.identity, Time.deltaTime * rotateBackAmmount);
            }
            return;
        }

        for (int i = 0; i < hooks.Count; i++)
        {
            // Apply rotation
            hooks[i].localEulerAngles -= rotateDirection * rotateAmmount * Time.deltaTime;

            // Normalize angles to the range of -180 to 180 degrees
            Vector3 eulerAngles = hooks[i].localEulerAngles;
            eulerAngles.x = NormalizeAngle(eulerAngles.x);
            eulerAngles.y = NormalizeAngle(eulerAngles.y);

            // Clamp angles within the specified range
            float tempHookX = Mathf.Clamp(eulerAngles.x, -maxXRot, maxXRot);
            float tempHookY = Mathf.Clamp(eulerAngles.y, -maxYRot, maxYRot);

            // Apply the clamped angles back to the hook
            hooks[i].localEulerAngles = new Vector3(tempHookX, tempHookY, eulerAngles.z);
        }
    }

    /// <summary>
    /// Makes an angle normalized between -180 and 180 as to not cause overflow errors when clamping angles
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180) angle -= 360;
        else if (angle < -180) angle += 360;
        return angle;
    }
}
