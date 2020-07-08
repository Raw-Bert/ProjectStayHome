using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        static public bool isDialogInProgress { get; private set; } = false;
        private GameObject currPopup, inst;
        private List<GameObject> dialogBoxes = new List<GameObject>();
        private static List<TextBoxData> dialogData = new List<TextBoxData>();

        public static void AddNextDialog(TextBoxData msg) => dialogData.Add(msg);
        public static void AddNextDialog(string dialogFile)
        {
            try
            {

                using(StreamReader stream = new StreamReader(Application.dataPath + "/" + dialogFile))
                {
                    TextBoxData data = null;
                    string textBody = "";

                    for (string line;
                        (line = stream.ReadLine()) != null;)
                    {
                        bool newBox = false;

                        //This is for line comments found in file
                        while (line.Contains("#"))
                            if ((line[line.LastIndexOf('#')].ToString() ?? "") != "\\")
                                line = line.Substring(0, line.LastIndexOf('#'));

                        //Find Name
                        if (line.Trim().Length > 0)
                            if (line.Trim()[0] == '[')
                            {
                                if (data != null)
                                    AddNextDialog(data);
                                textBody = "";
                                data = new TextBoxData();
                                data.name = line.Trim().Substring(1, line.Trim().IndexOf(']') - 1);
                                if (data.name.Length < 1)
                                    data.name = null;
                                continue;
                            }

                      
                        if (line.Replace(" ","").ToLower().Contains("textboxtype="))
                        {
                            string tmp = line.Replace(" ","").ToLower();
                            switch (tmp.Substring(tmp.IndexOf('=') + 1))
                            {
                                case "narration":
                                    data.textBoxType = TextBoxType.Narration;
                                    break;
                                case "dialog":
                                    data.textBoxType = TextBoxType.Dialog;
                                    break;
                                case "internal":
                                    data.textBoxType = TextBoxType.Internal;
                                    break;
                            }
                            continue;
                        }

                        //separate the dialog into sections
                        if (line.Trim() == "" || line.Trim() == @"\endsection")
                            newBox = true;
                        else
                            textBody += line;

                        if (newBox)
                        {
                            if (data != null)
                                if (textBody != "")
                                    data.addDialogSection(textBody);

                            textBody = "";
                            newBox = false;
                        }

                    }
                    if (data != null)
                    {
                        if (data != null)
                            if (textBody != "")
                                data.addDialogSection(textBody);

                        AddNextDialog(data);
                    }
                }
            }
            catch (Exception e) { print("Could not parse dialog:\n" + e); }
        }

        //this is for testing purposes
        void Start()
        {
            //Make sure you have 'using TextBoxSystem;' 
            //for this to work in other classes

            AddNextDialog("Dialog/test dialog.dia");

            TextBoxData tmp = new TextBoxData();
            tmp.textBoxType = TextBoxType.Narration;
            tmp.addDialogSection("My room looks really messy.");
            tmp.addDialogSection("Maybe I should clean this place up.");
            CreateDialog.AddNextDialog(tmp);

            tmp = new TextBoxData();
            tmp.textBoxType = TextBoxType.Dialog;
            tmp.name = "Protag";
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
                //the dialog is now in progress
                isDialogInProgress = true;

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
            else
                //Dialog nolonger in progress;
                isDialogInProgress = false;
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