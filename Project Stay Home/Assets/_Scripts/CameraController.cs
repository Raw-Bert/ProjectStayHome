using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Desk;

    static public bool isclickedTable;
    static public bool isZoom;

    private Vector3 myPos;
    private Vector3 myRot;

    private void Start()
    {
        // Save camera origional position
        myPos = transform.position;
        myRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Zoom to the table when camera is not in the zoom mode and table is clicked
        if (!isZoom && isclickedTable)
        {
            // Claculate the correct position when zoom to table
            Vector3 tmpPos = Desk.transform.position;
            tmpPos.y += 10;

            // Claculate the correct rotation when zoom to table
            Vector3 tmpRot = Desk.transform.eulerAngles;
            tmpRot.x += 180;
            tmpRot.y += 90;
            tmpRot.z += 90;

            // Set the camera's position rotation and view size
            this.transform.position = tmpPos;
            this.transform.eulerAngles = tmpRot;
            this.GetComponent<Camera>().orthographicSize = 2;

            // Stop the player tumble the scene
            SceneTumble.tumbleEnable = false;

            // Enable mouse to drag objects
            DragObject.Dragble = true;

            // Set the click to false and set camera to zoom mode
            isclickedTable = false;
            isZoom = true;
        }

        // Exit the zoom mode when player click the ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Set camera back to the original position, rotation and view size
            this.transform.position = myPos;
            this.transform.eulerAngles = myRot;
            this.GetComponent<Camera>().orthographicSize = 12;

            // Enable player to tumble the scence
            SceneTumble.tumbleEnable = true;

            // Stop mnouse to drag objects
            DragObject.Dragble = false;

            // Set the zoom mode to false
            isZoom = false;
        }
    }
}
