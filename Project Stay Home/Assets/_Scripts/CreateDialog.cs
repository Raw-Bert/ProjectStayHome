using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
namespace TextBoxSystem
{
    public class CreateDialog : MonoBehaviour
    {
        public GameObject popupPrefab;

        [Tooltip("Time it takes to fade to nothing")]
        public float fadeoutTime;
        private GameObject currPopup, inst;
        private List<GameObject> dialogBoxes = new List<GameObject>();
        private static List<TextBoxData> dialogLines = new List<TextBoxData>();

        public static void AddNextDialog(TextBoxData msg) => dialogLines.Add(msg);

        //this is for testing purposes
        void Start()
        {
            TextBoxData tmp = new TextBoxData();
            tmp.addDialogSection("this is a test of the abilities of the box thingy");
            tmp.addDialogSection("it could work... probably not though");
            AddNextDialog(tmp);
            tmp = new TextBoxData();
            tmp.addDialogSection("OK here is a new one");
            tmp.addDialogSection("I moved it so it looks cooler");
            tmp.position = new float2(250, 95);
            AddNextDialog(tmp);

        }

        // Update is called once per frame
        void Update()
        {
            while (dialogLines.Count > 0)
            {
                inst = Instantiate(popupPrefab, transform, false);
                inst.SetActive(false);
                //inst.GetComponent<FadeInOut>().delay = delay;
                inst.GetComponent<FadeInOut>().fadeoutTime = fadeoutTime;

                var data = inst.AddComponent<TextBoxData>();
                data.initData(dialogLines[0]);
                //set initial text
                if (dialogLines[0].getDialogSections().Count > 0)
                    inst.GetComponentInChildren<TMP_Text>().text = dialogLines[0].getDialogSection(0);

                if (inst.transform.parent != transform)
                    inst.GetComponent<RectTransform>().anchorMax =
                    inst.GetComponent<RectTransform>().anchorMin =
                    new Vector2(0, 0);

                inst.GetComponent<RectTransform>().anchoredPosition = dialogLines[0].position;
                inst.GetComponent<RectTransform>().localScale = Vector3.one;
                currPopup = inst;
                dialogBoxes.Add(inst);
                dialogLines.RemoveAt(0);
            }

            if (dialogBoxes.Count > 0)
            {
                if (dialogBoxes[0] == null)
                {
                    dialogBoxes.RemoveAt(0);
                    return;
                }
                var textbox = dialogBoxes[0].GetComponent<TextBoxData>();
                var thetext = dialogBoxes[0].GetComponentInChildren<TMP_Text>();

                // if (!dialogBoxes[0].activeInHierarchy)
                if (!dialogBoxes[0].activeInHierarchy)
                    dialogBoxes[0].SetActive(true);

                //TODO: switch the text after each section is complete
                if (textbox.isSectionComplete)
                    if (textbox.canProgress)
                    {
                        textbox.increaseCounter();

                        if (textbox.getCounter() < textbox.getDialogSections().Count)
                            thetext.text = textbox.getDialogSection();
                        else
                            dialogBoxes[0].GetComponent<FadeInOut>().fadeOutInvoke();

                        textbox.canProgress = false;
                    }

                if (dialogBoxes[0].GetComponent<CanvasGroup>().alpha == 0)
                {
                    Destroy(dialogBoxes[0]);
                    //popups[index] = null;
                }
                else
                {
                    dialogBoxes[0].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                        Mathf.Lerp(0, popupPrefab.GetComponent<RectTransform>().sizeDelta.y,
                            dialogBoxes[0].GetComponent<CanvasGroup>().alpha));
                }
                // popups[index].transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }
    }
}