using System.Collections;
using System.Collections.Generic;
using TextBoxSystem;
using UnityEngine;

public class DeskInteraction : MonoBehaviour
{
    public bool interactableDesk = true;

    private void OnMouseDown()
    {
        // Trigger camera zoom in to the desk when the camera is not in the 
        // zoom mode and player clicked the desk
        if(interactableDesk)
            if (!CameraController.isZoom && !CreateDialog.isDialogInProgress)
                CameraController.isclickedTable = true;
    }
}
