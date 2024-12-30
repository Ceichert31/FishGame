using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    //Write text and slow game 
    //wait until a button is pressed

    bool firstParry;

    public void SetTutorial(IntEvent tutorialNum)
    {
        switch (tutorialNum.Value)
        {
            case 0:
                FirstParry();
                break;
        }
    }

    void FirstParry()
    {
        if (firstParry) return;
    }
}
