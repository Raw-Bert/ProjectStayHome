using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TextBoxSystem
{
    public class TextBoxController : MonoBehaviour
    {
        public TextBoxData data;
        int m_counter { get; set; } = 0;
        RawImage continueMarker;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            continueMarker = GetComponentInChildren<RawImage>();
        }
        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (data == null)
                return;

            //check if the text reveal is complete
            data.isSectionComplete = GetComponentInChildren<TextReveal>().isRevealComplete;

            //set marker active when section is complete
            continueMarker.gameObject.SetActive(data.isSectionComplete);

            if (gameObject.activeInHierarchy)
                if (data.isSectionComplete)
                    //move to next section
                    data.canProgress = Input.GetMouseButtonDown(0);
                else
                    //fast dorward to end of text
                    GetComponentInChildren<TextReveal>().setTextComplete = Input.GetMouseButtonDown(0);

        }
   

        /// <summary>
        /// MUST be called for any function to work 
        /// </summary>
        public void initData(TextBoxData data)
        {
            this.data = data;
        }

        /// <summary>
        /// Get section counter specifying which section is currently showing
        /// </summary>
        public int getCounter() => m_counter;
        public void increaseCounter() => ++m_counter;
        public void decreaseCounter() => --m_counter;

        public void addDialogSection(string txt) => data.addDialogSection(txt);

        /// <summary>
        /// Get text of current dialog section 
        /// </summary>

        public string getDialogSection() => data.getDialogSection(m_counter);
        /// <summary>
        /// Get text of dialog section specified by index 
        /// </summary>
        public string getDialogSection(int index) => data.getDialogSection(index);

        /// <summary>
        /// Get List of dialog sections 
        /// </summary>

        public List<string> getDialogSections() => data.getDialogSections();

    }
}