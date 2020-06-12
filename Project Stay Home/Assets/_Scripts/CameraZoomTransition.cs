using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraZoomTransition : MonoBehaviour
{
    private bool isZoomed = false;
    public string scene;
    public GameObject clickableObject;

    
    // Update is called once per frame
    void Update()
    {
        if (clickableObject.GetComponent<ObjectClick>().isPressed)
        {
            //zoom camera in
            //position lerp vector3(this.position, clickableObject.position.25f) 
            this.transform.position = Vector3.Lerp(this.transform.position, clickableObject.transform.position, .25f);
            //if (this.transform.position == clickableObject.transform.position)
            isZoomed = true;
        }

        if (isZoomed)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
    
    
}
