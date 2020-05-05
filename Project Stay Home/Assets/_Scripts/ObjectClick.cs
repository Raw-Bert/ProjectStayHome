using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    public Vector3 minScale;
    Vector3 currentScale;
     //private Vector3 maxScale;
    //private Vector3 targetScale;
     //public float targetScaleAfter;
     public Vector3 maxScale;
     public bool isPressed = false;

     // Start is called before the first frame update
    void Start()
    {
        //targetScale = transform.localScale;
        //minScale = transform.localScale;
        //maxScale = targetScale * targetScaleAfter;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnMouseEnter()
    {
        Debug.Log("Mouse is over GameObject");
        //minScale = transform.localScale;
        transform.localScale = Vector3.Lerp(minScale, maxScale, .25f);
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse is no longer over GameObject");
        currentScale = transform.localScale;
        transform.localScale = Vector3.Lerp(currentScale, minScale, .25f);
    }

    //When mouse pressed over object with collider
    void OnMouseDown()
    {
        isPressed = true;
    }

}