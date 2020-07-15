using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FMOD.Studio;
using FMODUnity;

public class DragObject : MonoBehaviour
{
    public bool activeDragRotation = false;
    public Vector3 dragRotation = new Vector3(0, 0, 0);

    Vector3 mouseOffSet;
    Vector3 horizontalOffSet = new Vector3(0, 2, 0);
    Vector3 originalPos;
    Quaternion originalRot;

    public bool inRightPlace;
    static public bool Dragble = false;

    public GameObjectType types;

    bool pickUpPlayable = true;

    public enum GameObjectType
    {
        None,
        Garbage,
        Pen,
        Book
    }

    void Start()
    {
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "floor")
        {
            Invoke("backToOriginal", 1);
        }

        if (types == GameObjectType.Pen)
        {
            if (other.tag == "garbageCan" || other.tag == "storageBin")
            {
                Invoke("backToOriginal", 1);

                FMODUnity.RuntimeManager.PlayOneShot("event:/Master/Sound FX/Wrong Place");
            }
            if (other.tag == "pencilHolder")
            {
                inRightPlace = true;

                FMODUnity.RuntimeManager.PlayOneShot("event:/Master/Sound FX/Right Place");
            }
        }
        if (types == GameObjectType.Book)
        {
            if (other.tag == "garbageCan")
            {
                Invoke("backToOriginal", 1);

                FMODUnity.RuntimeManager.PlayOneShot("event:/Master/Sound FX/Wrong Place");
            }
            if (other.tag == "storageBin")
            {
                inRightPlace = true;

                FMODUnity.RuntimeManager.PlayOneShot("event:/Master/Sound FX/Right Place");
            }
        }
        if (types == GameObjectType.Garbage)
        {
            if (other.tag == "storageBin")
            {
                Invoke("backToOriginal", 1);

                FMODUnity.RuntimeManager.PlayOneShot("event:/Master/Sound FX/Wrong Place");
            }
            if (other.tag == "garbageCan")
            {
                inRightPlace = true;

                FMODUnity.RuntimeManager.PlayOneShot("event:/Master/Sound FX/Right Place");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (types == GameObjectType.Pen)
        {
            if (other.tag == "pencilHolder")
                inRightPlace = false;
        }
        if (types == GameObjectType.Book)
        {
            if (other.tag == "storageBin")
                inRightPlace = false;
        }
        if (types == GameObjectType.Garbage)
        {
            if (other.tag == "garbageCan")
                inRightPlace = false;
        }
    }

    private void OnMouseDown()
    {
        // Calculate the mouse offset between this object to the mouse position
        mouseOffSet = gameObject.transform.position - MouseWorldPos();
    }
    
    private void OnMouseDrag()
    {
        // Set this object to thr relevant mourse position when drag is enabled
        if (Dragble)
        {
            if (types == GameObjectType.Garbage)
                DeskCleaningGameManager.showGBCircle = true;
            if (types == GameObjectType.Pen)
                DeskCleaningGameManager.showPHCircle = true;
            if (types == GameObjectType.Book)
                DeskCleaningGameManager.showSBSquare = true;

            transform.position = MouseWorldPos() + mouseOffSet + horizontalOffSet;
            if (activeDragRotation)
                transform.localEulerAngles = dragRotation;

            if (pickUpPlayable)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Master/Sound FX/Pick Up");
                pickUpPlayable = false;
            }
        }
    }

    private void OnMouseUp()
    {
        DeskCleaningGameManager.showGBCircle = false;
        DeskCleaningGameManager.showPHCircle = false;
        DeskCleaningGameManager.showSBSquare = false;

        pickUpPlayable = true;
    }

    private Vector3 MouseWorldPos()
    {
        // Calculate the mouse world position
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void backToOriginal()
    {
        transform.localPosition = originalPos;
        transform.localRotation = originalRot;
    }
}
