using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PhoneGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Button cameraButton;
    public Button mediaButton;
    public Image picture1;
    public Image picture2;
    public Image picture3;
    public Image mediaPage;
    public int buttonPresses = 0;

    //Make first picture the active one.
    void Awake() {
        picture1.enabled = true;
        picture2.enabled = false;
        picture3.enabled = false;
        mediaPage.enabled = false;
    }

    //Enables and Disables buttons and pictures each time camera button clicked
    //Switches to media page after 3 clicks
    public void CameraOnClick()
    {
        Debug.Log("WTF");
        if (buttonPresses == 0)
        {
            //Switch to second picture
            picture1.enabled = false;
            picture2.enabled = true;
            buttonPresses += 1;
        }
        else if (buttonPresses == 1)
        {
            //Switch to third picture
            picture2.enabled = false;
            picture3.enabled = true;
            buttonPresses += 1;
        }
        else if (buttonPresses == 2)
        {
            //Switch to media page
            picture3.enabled = false;
            mediaPage.enabled = true;
            cameraButton.gameObject.SetActive(false);
            mediaButton.gameObject.SetActive(true);
            buttonPresses += 1;
        }
    }

    //"posts" grad pictures to media, minigame ends after this
    public void MediaOnPost()
    {
        mediaPage.enabled = false;
        Debug.Log("YEEEEEET");
    }
}
