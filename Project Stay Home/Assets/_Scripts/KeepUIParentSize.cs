using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUIParentSize : MonoBehaviour
{


    [Tooltip("if no object is set the parent is selected")]
    public GameObject canvas;
    public Vector2 offset;
    // Update is called once per frame
    void Update()
    {
        if (!canvas)
            canvas = transform.parent.gameObject;

        var rt1 = canvas.GetComponent<RectTransform>().sizeDelta;
        var rt2 = gameObject.GetComponent<RectTransform>().sizeDelta;
        transform.localScale = rt1 / rt2;

        GetComponent<RectTransform>().anchoredPosition = offset;
    }
}