using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTumble : MonoBehaviour
{
    Vector3 mPreviousPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public GameObject light;
    static public bool tumbleEnable = true;
    // Update is called once per frame
    void Update()
    {
        if (tumbleEnable)
        {
            if (Input.GetMouseButton(0))
            {
                mPosDelta = Input.mousePosition - mPreviousPos;
                if (Vector3.Dot(transform.up, Vector3.up) >= 0)
                {
                    transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                }
                else
                {
                    transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                }
                //This is the line for vertical spinning
                //transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up), Space.World);
                light.transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }
        }

        mPreviousPos = Input.mousePosition;
    }
}
