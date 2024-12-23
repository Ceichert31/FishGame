using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum to mimic boolean behavior (False = 0, True = 1)
enum Bools
{
    False = 0,
    True = 1,
}

public class SheenController : MonoBehaviour
{
    [SerializeField] Material parrySheen;
    const float DEFAULTVALUE = -0.31f;        // Starting value for the sheen
    const float ENDVALUE = 0.15f;             // Final value for the sheen
    [SerializeField] float multiplier = 1; 

    void Start()
    {
        parrySheen.SetFloat("_Control", DEFAULTVALUE); // Initialize sheen control
    }

    [ContextMenu("Test Sheen")]
    public void TestSheen()
    {
        StartSheen(new());
    }

    /// <summary>
    /// Callable from a Float event listener to start the shee effect
    /// </summary>
    /// <param name="ctx"></param>
    public void StartSheen(FloatEvent ctx)
    {
        StopAllCoroutines();
        ParrySheenSet(Caster(Bools.True));
        StartCoroutine(CommenceSheen());
    }

    /// <summary>
    /// Moves the value of the shader to induce a sheen effect
    /// </summary>
    /// <returns></returns>
    IEnumerator CommenceSheen()
    {
        float currentValue = DEFAULTVALUE;
        while (currentValue < ENDVALUE)
        {
            currentValue += Time.deltaTime * multiplier; 
            parrySheen.SetFloat("_Control", currentValue);
            yield return null;
        }
        ParrySheenSet(Caster(Bools.False));
    }

    private void OnDisable()
    {
        ParrySheenSet(Caster(Bools.False));
    }

    /// <summary>
    /// Method for repeatedly setting material values
    /// </summary>
    /// <param name="trueFalse"></param>
    void ParrySheenSet(int trueFalse)
    {
        parrySheen.SetFloat("_Boolean", trueFalse);
        parrySheen.SetFloat("_Control", DEFAULTVALUE);
    }

    int Caster(Bools value)
    {
        return (int)value;
    }
}
