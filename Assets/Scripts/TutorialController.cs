using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    //Popup window with instructions
    //E to continue?

    //More imersive method??

    bool firstShot = true;

    public void SetTutorial(IntEvent tutorialID)
    {
        switch (tutorialID.Value)
        {
            case 0:
                if (!firstShot) break;
                //Popup "Press LMB to fire"
                firstShot = false;

                break;
        }
    }
}
