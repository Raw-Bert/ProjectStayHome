﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
namespace TextBoxSystem
{
    public class CreateDialog : MonoBehaviour
    {
        public GameObject dialogPrefab, narrationPrefab;

        [Tooltip("Time it takes to fade to nothing")]
        public float fadeoutTime;
        private GameObject currPopup, inst;
        private List<GameObject> dialogBoxes = new List<GameObject>();
        private static List<TextBoxData> dialogData = new List<TextBoxData>();

        public static void AddNextDialog(TextBoxData msg) => dialogData.Add(msg);

        //this is for testing purposes
        void Start()
        {
            //Make sure you have 'using TextBoxSystem;' 
            //for this to work in other classes

            TextBoxData tmp = new TextBoxData();
            tmp.textBoxType = TextBoxType.Narration;
            tmp.addDialogSection("this is a test of the abilities of the text box thingy");
            tmp.addDialogSection("it could work... probably not though");
            CreateDialog.AddNextDialog(tmp);

            tmp = new TextBoxData();
            tmp.textBoxType = TextBoxType.Dialog;
            tmp.name = "EmFresh";
            tmp.addDialogSection("OK here is a new one");
            tmp.addDialogSection("I moved it so it looks cooler");
            tmp.setMovement(0, MovementType.Breath);
            tmp.setMovement(1, MovementType.None);
            tmp.position = new float2(250, 150);
            CreateDialog.AddNextDialog(tmp);

            tmp = new TextBoxData();
            tmp.textBoxType = TextBoxType.Internal;
            tmp.setMovement(0, MovementType.Breath);
            tmp.addDialogSection("Who am I?");
            CreateDialog.AddNextDialog(tmp);

        }

        // Update is called once per frame
        void Update()
        {
            /* ***************
             * TEXT BOX INIT *
             *************** */
            while (dialogData.Count > 0)
            {

                //Instantiate the proper textbox
                switch (dialogData[0].textBoxType)
                {
                    case TextBoxType.Dialog:
                        inst = Instantiate(dialogPrefab, transform, false);
                        break;
                    case TextBoxType.Internal:
                        inst = Instantiate(dialogPrefab, transform, false);
                        inst.GetComponentsInChildren<TMP_Text>()[1].fontStyle =
                            inst.GetComponentInChildren<TMP_Text>().fontStyle | FontStyles.Italic;
                        break;
                    case TextBoxType.Narration:
                        inst = Instantiate(narrationPrefab, transform, false);
                        break;

                }

                inst.SetActive(false); //make sure instance is not active on start
                inst.GetComponent<FadeInOut>().fadeoutTime = fadeoutTime;

                //add a TextBoxController to new instance (or keep an already existing one)
                var data = inst.GetComponent<TextBoxController>() ?? inst.AddComponent<TextBoxController>();
                data.initData(dialogData[0]);

                //set what type of text effect is applied
                if (dialogData[0].movement.Count > 0)
                {
                    inst.GetComponentInChildren<TextMovement>(). //
                    movement = dialogData[0].movement[0];
                }

                //set initial text
                if (dialogData[0].getDialogSections().Count > 0)
                {
                    switch (dialogData[0].textBoxType)
                    {
                        case TextBoxType.Dialog:
                        case TextBoxType.Internal:

                            TMP_Text[] textFields = inst.GetComponentsInChildren<TMP_Text>();
                            foreach (var text in textFields)
                            {
                                if (text.gameObject.name.ToLower().Contains("name"))
                                    text.text = (dialogData[0].name ?? "???") + ':';
                                else if (text.gameObject.name.ToLower().Contains("dialog"))
                                    text.text = dialogData[0].getDialogSection(0);
                            }
                            break;

                        case TextBoxType.Narration:
                            TMP_Text textField = inst.GetComponentInChildren<TMP_Text>();
                            textField.text = dialogData[0].getDialogSection(0);
                            break;

                    }
                }

                //place ancor point in the bottom left corner
                if (inst.transform.parent != transform)
                    inst.GetComponent<RectTransform>().anchorMax =
                    inst.GetComponent<RectTransform>().anchorMin =
                    new Vector2(0, 0);

                //set the text location based on anchor point 
                inst.GetComponent<RectTransform>().anchoredPosition = dialogData[0].position;
                inst.GetComponent<RectTransform>().localScale = Vector3.one;

                currPopup = inst;
                dialogBoxes.Add(inst);
                dialogData.RemoveAt(0);
            }

            /* ****************
             * TEXT BOX LOGIC *
             **************** */
            if (dialogBoxes.Count > 0)
            {
                //remove null objects from list
                if (dialogBoxes[0] == null)
                {
                    dialogBoxes.RemoveAt(0);
                    return;
                }

                var textbox = dialogBoxes[0].GetComponent<TextBoxController>();
                var thetext = dialogBoxes[0].GetComponentInChildren<TMP_Text>();
                var txtmove = dialogBoxes[0].GetComponentInChildren<TextMovement>();

                switch (textbox.data.textBoxType)
                {
                    case TextBoxType.Dialog:
                    case TextBoxType.Internal:

                        foreach (var text in dialogBoxes[0].GetComponentsInChildren<TMP_Text>())
                            if (text.gameObject.name.ToLower().Contains("dialog"))
                                thetext = text;

                        break;
                }

                //Make sure the text box is enabled 
                if (!dialogBoxes[0].activeInHierarchy)
                    dialogBoxes[0].SetActive(true);

                //switch the text after each section is complete
                if (textbox.data.isSectionComplete)
                    if (textbox.data.canProgress)
                    {
                        textbox.increaseCounter();

                        if (textbox.getCounter() < textbox.data.movement.Count)
                            txtmove.movement = textbox.data.movement[textbox.getCounter()];

                        if (textbox.getCounter() < textbox.getDialogSections().Count)
                            thetext.text = textbox.getDialogSection();
                        else
                            dialogBoxes[0].GetComponent<FadeInOut>().fadeOutInvoke();

                        textbox.data.canProgress = false;
                    }

                //Make the textboxes shrink and fadeout 
                if (dialogBoxes[0].GetComponent<CanvasGroup>().alpha == 0)
                {
                    //this will destroy the current text box and move on to the next
                    Destroy(dialogBoxes[0]);
                }
                else
                {
                    //lerp from height to current anchor
                    dialogBoxes[0].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                        Mathf.Lerp(0, dialogPrefab.GetComponent<RectTransform>().sizeDelta.y,
                            dialogBoxes[0].GetComponent<CanvasGroup>().alpha));
                }
            }
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        void OnDestroy()
        {
            dialogBoxes.Clear();
        }
    }
}