using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ClothsPickupMinigame : MonoBehaviour
{
    public GameObject canvas;

    List<GameObject> clothing = new List<GameObject>();
    List<GameObject> icons = new List<GameObject>();

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        GameObject cloths, basket;

        //left click || keycode
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            //Pick up clothing
            if ((cloths = other.gameObject).tag.ToLower().Contains("cloths"))
            {
                if (!cloths.GetComponent<Rigidbody>())
                {
                    clothingPickup(cloths);
                    clothing.Add(cloths);
                    cloths.SetActive(false);
                }
            }
            //put them in basket
            else if ((basket = other.gameObject).tag.ToLower().Contains("basket"))
            {
                clothingDrop(basket);
                clothing.RemoveRange(0, clothing.Count);
            }
        }
    }
    void clothingPickup(GameObject cloths)
    {
        var ui = cloths.GetComponentInChildren<RawImage>();
        var uiTrans = ui.GetComponent<RectTransform>();

        ui.transform.SetParent(canvas.transform);
        uiTrans.position = Vector3.zero;
        uiTrans.rotation = Quaternion.identity;
        uiTrans.localScale = Vector3.one;
        uiTrans.ForceUpdateRectTransforms();

        uiTrans.anchoredPosition = new float2(0, 0);
        if (canvas.transform.childCount > 2)
        {
            var lastItem = canvas.transform.GetChild(canvas.transform.childCount - 2).GetComponent<RectTransform>();
            uiTrans.anchoredPosition = (float2)lastItem.anchoredPosition + new float2(lastItem.sizeDelta.x, 0);
        }
        icons.Add(ui.gameObject);
    }

    void clothingDrop(GameObject basket)
    {
        int count = 0;
        foreach (var cloth in clothing)
        {
            cloth.transform.position = (float3)basket.transform.position +
                (GetComponent<Collider>().bounds.max * new float3(0, 1, 0)) +
                (float3)cloth.GetComponent<Collider>().bounds.max * new float3(0, 1, 0) *
                count++;

            var collider = cloth.AddComponent<MeshCollider>();
            collider.convex = true;
            cloth.AddComponent<Rigidbody>();
            cloth.SetActive(true);
        }
        foreach(var icon in icons)
            Destroy(icon);
        icons.Clear();
    }
}