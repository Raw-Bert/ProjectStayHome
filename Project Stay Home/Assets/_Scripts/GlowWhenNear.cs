using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowWhenNear : MonoBehaviour
{
    // Start is called before the first frame update
    public bool glow = false;
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("MainCharacter"))
        {
            glow = true;
            Debug.Log("GLOW");
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag("MainCharacter"))
        {
            glow = false;
            Debug.Log("NO MORE GLOW");
        }
    }
}
