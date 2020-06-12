using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibleController : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;

    // Update is called once per frame
    void Update()
    {
        //Set the visibility of wall 1
        if(this.transform.eulerAngles.y < 45)
        {
            wall1.SetActive(true);
        }
        else if(this.transform.eulerAngles.y > 225)
        {
            wall1.SetActive(true);
        }
        else
        {
            wall1.SetActive(false);
        }

        //Set the visibility of wall 2
        if (this.transform.eulerAngles.y < 135)
        {
            wall2.SetActive(true);
        }
        else if (this.transform.eulerAngles.y > 315)
        {
            wall2.SetActive(true);
        }
        else
        {
            wall2.SetActive(false);
        }

        //Set the visibility of wall 3
        if (this.transform.eulerAngles.y > 225)
        {
            wall3.SetActive(false);
        }
        else if (this.transform.eulerAngles.y < 45)
        {
            wall3.SetActive(false);
        }
        else
        {
            wall3.SetActive(true);
        }

        //Set the visibility of wall 4
        if (this.transform.eulerAngles.y > 315)
        {
            wall4.SetActive(false);
        }
        else if (this.transform.eulerAngles.y < 135)
        {
            wall4.SetActive(false);
        }
        else
        {
            wall4.SetActive(true);
        }
    }
}
