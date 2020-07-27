using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ClothsPickupMinigame : MonoBehaviour
{
    List<GameObject> clothing = new List<GameObject>();

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        GameObject cloths, basket;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            //Pick up clothing
            if ((cloths = other.gameObject).tag.ToLower().Contains("cloths"))
            {
                if (!cloths.GetComponent<Rigidbody>())
                {
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

    void clothingDrop(GameObject basket)
    {
        int count = 0;
        foreach (var cloth in clothing)
        {
            cloth.transform.position = (float3)basket.transform.position +
                (GetComponent<Collider>().bounds.max * new float3(0, 1, 0)) +
                (float3)cloth.GetComponent<Collider>().bounds.max * new float3(0, 1, 0) *
                count++;
            cloth.AddComponent<Rigidbody>();
            cloth.SetActive(true);
        }
    }
}