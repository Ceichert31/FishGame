using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestValuePrint : MonoBehaviour
{
    public void PrintNumber(IntEvent ctx)
    {
        Debug.Log(ctx.Value);
    }
}
