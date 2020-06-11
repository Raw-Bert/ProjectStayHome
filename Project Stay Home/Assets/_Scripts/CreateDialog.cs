using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateDialog : MonoBehaviour
{
    public GameObject popupPrefab;
    public float delay, fadeoutTime;
    private GameObject currPopup, inst;
    private List<GameObject> popups = new List<GameObject>();
    private static List<TextBoxData> dialogLines;

    public struct TextBoxData
    {
        List<string> sections;
        Vector2 position;
        public void addDialogSection(string txt) => sections.Add(txt);
        public string getDialogSection(int index) => sections[index];
        public List<string> getDialogSections() => sections;
    }

    public static void AddNextDialog(TextBoxData msg) => dialogLines.Add(msg);

    // Update is called once per frame
    void Update()
    {
        while (dialogLines.Count > 0)
        {
            inst = Instantiate(popupPrefab, inst == null ? transform : currPopup.transform, false);
            inst.GetComponent<FadeInOut>().delay = delay;
            inst.GetComponent<FadeInOut>().fadeoutTime = fadeoutTime;

            if (dialogLines.Count > 0)
                inst.GetComponentInChildren<TMP_Text>().text = dialogLines[0].getDialogSection(0);

            if (inst.transform.parent != transform)
                inst.GetComponent<RectTransform>().anchorMax =
                inst.GetComponent<RectTransform>().anchorMin =
                new Vector2(.5f, 0);

            inst.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            inst.GetComponent<RectTransform>().localScale = Vector3.one;
            currPopup = inst;
            popups.Add(inst);
            dialogLines.RemoveAt(0);
        }

        for (int index = 0; index < popups.Count; ++index)
        {
            if (popups[index] == null)
            {
                popups.RemoveAt(index--);
                continue;
            }

            if (popups[index].GetComponent<CanvasGroup>().alpha == 0)
            {
                if (popups[index].transform.childCount > 1)
                {
                    var child = popups[index].transform.GetChild(1);
                    popups[index].transform.GetChild(1).SetParent(popups[index].transform.parent, false);
                    if (child.parent == transform)
                        child.GetComponent<RectTransform>().anchorMax =
                        child.GetComponent<RectTransform>().anchorMin =
                        new Vector2(.5f, 1);

                    child.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                Destroy(popups[index]);
                //popups[index] = null;
            }
            else
            {
                popups[index].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                    Mathf.Lerp(0, popupPrefab.GetComponent<RectTransform>().sizeDelta.y,
                        popups[index].GetComponent<CanvasGroup>().alpha));
            }
            // popups[index].transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

}