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
    void Start() {
        
    }
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
            //wall2.SetActive(true);
            Color wall2Color = wall2.GetComponent<MeshRenderer>().material.color;
            wall2Color.a = 1.0f;
            wall2.GetComponent<MeshRenderer>().material.color = wall2Color;
            Debug.Log(wall2.GetComponent<MeshRenderer>().material.color.a);
        }
        else if (this.transform.eulerAngles.y > 315)
        {
            //wall2.SetActive(true);
            Color wall2Color = wall2.GetComponent<MeshRenderer>().material.color;
            wall2Color.a = 1.0f;
            wall2.GetComponent<MeshRenderer>().material.color = wall2Color;
            Debug.Log(wall2.GetComponent<MeshRenderer>().material.color.a);
            
        }
        else
        {
           // wall2.SetActive(false);
            Color wall2Color = wall2.GetComponent<MeshRenderer>().material.color;
            wall2Color.a = 0.0f;
            wall2.GetComponent<MeshRenderer>().material.color = wall2Color;
            Debug.Log(wall2.GetComponent<MeshRenderer>().material.color.a);
            //wall2.GetComponent<MeshRenderer>().material.color.a = wall2Color;
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
