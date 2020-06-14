using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextReveal : MonoBehaviour
{
    public float speed = 5, minspeed = 50;
    public bool revealComplete;
    bool hasTextChanged;
    TMP_Text textToReveal;

    // Start is called before the first frame update
    void Start()
    {
        textToReveal = GetComponent<TMP_Text>() ?? gameObject.AddComponent<TMP_Text>();

        StartCoroutine(textReveal());
    }

    void OnEnable()
    {
        // Subscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }

    void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }
    string lastStr;
    void ON_TEXT_CHANGED(Object obj)
    {
        if (obj == textToReveal)
            if (lastStr != textToReveal.text)
            {
                lastStr = textToReveal.text;

                for (int index = 0; index < textToReveal.textInfo.meshInfo.Length; index++)
                {
                    for (int index2 = 0; index2 < textToReveal.textInfo.meshInfo[index].colors32.Length; index2++)
                        textToReveal.textInfo.meshInfo[index].colors32[index2].a = 0;

                }
                textToReveal.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                hasTextChanged = true;
            }
    }
    IEnumerator textReveal()
    {

        textToReveal.ForceMeshUpdate();
        float percent = 0;

        yield return new WaitForSeconds(.1f);
        
        int currentChar = 0;
        while (true)
        {

            if (hasTextChanged)
            {
                currentChar = 0;
                revealComplete = false;
                
                yield return new WaitForSeconds(0.1f);
               
                hasTextChanged = false;
                continue;
            }
            
            if (currentChar >= textToReveal.textInfo.characterCount)
            {
                revealComplete = true;
                yield return new WaitForSeconds(0.1f);
                continue;
            }

            int meshIndex = textToReveal.textInfo.characterInfo[currentChar].materialReferenceIndex;
            int vertIndex = textToReveal.textInfo.characterInfo[currentChar].vertexIndex;
            var colourReplace = textToReveal.textInfo.meshInfo[meshIndex].colors32;

            if (textToReveal.text[currentChar] != ' ')
            {
                if (percent <= .5f)
                {
                    colourReplace[vertIndex + 0].a = (byte)(percent * 2 * 255);
                    colourReplace[vertIndex + 1].a = (byte)(percent * 2 * 255);

                    colourReplace[vertIndex + 2].a = (byte)(0);
                    colourReplace[vertIndex + 3].a = (byte)(0);
                }
                else
                {
                    colourReplace[vertIndex + 0].a = (byte)(255);
                    colourReplace[vertIndex + 1].a = (byte)(255);

                    colourReplace[vertIndex + 2].a = (byte)((percent - .5f) * 2 * 255);
                    colourReplace[vertIndex + 3].a = (byte)((percent - .5f) * 2 * 255);
                }
            }
            else
            {
                currentChar++;
                percent -= Time.deltaTime * (speed * 2 + minspeed);
            }

            percent += Time.deltaTime * (speed * 2 + minspeed);
            if (percent >= 1)
            {

                for (int index = 0; index < (int)percent; index++)
                {
                    vertIndex = textToReveal.textInfo.characterInfo[currentChar + index].vertexIndex;

                    colourReplace[vertIndex + 0].a = (byte)(255);
                    colourReplace[vertIndex + 1].a = (byte)(255);
                    colourReplace[vertIndex + 2].a = (byte)(255);
                    colourReplace[vertIndex + 3].a = (byte)(255);
                }
                currentChar += (int)(percent);
                percent %= 1;
            }

            textToReveal.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            yield return new WaitForSeconds(0.1f);
        }
    }
}