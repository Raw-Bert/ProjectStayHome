using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibleController : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;

    void Update()
    {
        //Set the visibility of wall 1
        if (this.transform.eulerAngles.y < 45)
        {
            //wall2.SetActive(true);
            Color wall2Color = wall1.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 1.0f;
            wall1.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

        }
        else if (this.transform.eulerAngles.y > 225)
        {
            //wall2.SetActive(true);
            Color wall2Color = wall1.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 1.0f;
            wall1.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

        }
        else
        {
            //wall2.SetActive(true);
            Color wall2Color = wall1.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 0.0f;
            wall1.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

        }

        //Set the visibility of wall 2
        if (this.transform.eulerAngles.y < 135)
        {
            //wall2.SetActive(true);
            Color wall2Color = wall2.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 1.0f;
            wall2.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

            Debug.Log(wall2.GetComponent<MeshRenderer>().material.color.a);
        }
        else if (this.transform.eulerAngles.y > 315)
        {
            //wall2.SetActive(true);
            Color wall2Color = wall2.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 1.0f;
            wall2.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

            Debug.Log(wall2.GetComponent<MeshRenderer>().material.color.a);

        }
        else
        {
            Color wall2Color = wall2.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 0.0f;
            wall2.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

            Debug.Log(wall2.GetComponent<MeshRenderer>().material.color.a);
        }

        //Set the visibility of wall 3
        if (this.transform.eulerAngles.y > 225)
        {
            //wall2.SetActive(true);
            Color wall2Color = wall3.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 0.0f;
            wall3.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

        }
        else if (this.transform.eulerAngles.y < 45)
        {
            //wall2.SetActive(true);
            Color wall2Color = wall3.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 0.0f;
            wall3.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

        }
        else
        {
            //wall2.SetActive(true);
            Color wall2Color = wall3.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 1.0f;
            wall3.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

        }

        //Set the visibility of wall 4
        if (this.transform.eulerAngles.y > 315)
        {
            //wall2.SetActive(true);
            Color wall2Color = wall4.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 0.0f;
            wall4.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

        }
        else if (this.transform.eulerAngles.y < 135)
        {
            //wall2.SetActive(true);
            Color wall2Color = wall4.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 0.0f;
            wall4.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);
        }
        else
        {
            //wall2.SetActive(true);
            Color wall2Color = wall4.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
            wall2Color.a = 1.0f;
            wall4.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", wall2Color);

        }
    }
}