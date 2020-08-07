using System.Collections;
using System.Collections.Generic;
using TextBoxSystem;
using UnityEngine;

public class DeskInteraction : MonoBehaviour
{
    public bool interactableDesk = false;
    public GameObject hitBox;

    private void OnMouseDown()
    {
        // Trigger camera zoom in to the desk when the camera is not in the 
        // zoom mode and player clicked the desk
        if(interactableDesk)
            if (!CameraController.isZoom && !CreateDialog.isDialogInProgress)
                CameraController.isclickedTable = true;
    }
     void Update() {
        {
            if (hitBox.GetComponent<GlowWhenNear>().glow == true)
            {
                interactableDesk = true;
                Debug.Log(hitBox.GetComponent<GlowWhenNear>().glow);
            }
        }
    }
}
