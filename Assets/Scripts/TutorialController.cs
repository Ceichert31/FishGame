using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    //Popup window with instructions
    //E to continue?

    //More imersive method??

    bool firstShot = true;

    public void SetTutorial(KeyEvent tutorialValue)
    {
        switch (tutorialValue.ID)
        {
            case 0:
                if (!firstShot) break;
                FirstShot();
                break;
        }
    }

    void FirstShot()
    {
        //Popup "Press LMB to fire"
        firstShot = false;
    }
}
