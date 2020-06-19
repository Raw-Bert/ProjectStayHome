using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    Vector3 mouseOffSet;
    static public bool Dragble = false;

    private void OnMouseDown()
    {
        // Calculate the mouse offset between this object to the mouse position
        mouseOffSet = gameObject.transform.position - MouseWorldPos();
    }

    private void OnMouseDrag()
    {
        // Set this object to thr relevant mourse position when drag is enabled
        if (Dragble)
        transform.position = MouseWorldPos() + mouseOffSet;
    }

    private Vector3 MouseWorldPos()
    {
        // Calculate the mouse world position
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
