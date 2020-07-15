using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    [EventRef]
    public string dropEv = "";
    EventInstance instance;
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (physics only).
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        //if (instance.isValid())
        //{
        //    RuntimeManager.DetachInstanceFromGameObject(instance);
        //    instance.release();
        //    instance.clearHandle();
        //}
        instance = FMODUnity.RuntimeManager.CreateInstance(dropEv);
        instance.start();

        //  playerState.

    }
}