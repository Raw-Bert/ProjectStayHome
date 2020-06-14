using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TextBoxSystem
{
    public class TextBoxController : MonoBehaviour
    {
        int m_counter { get; set; } = 0;
        public TextBoxData data;

        public void initData(TextBoxData data)
        {
            this.data = data;
        }

        public int getCounter() => m_counter;
        public void increaseCounter() => ++m_counter;
        public void decreaseCounter() => --m_counter;

        public void addDialogSection(string txt) => data.addDialogSection(txt);
        public string getDialogSection() => data.getDialogSection(m_counter);
        public string getDialogSection(int index) => data.getDialogSection(index);
        public List<string> getDialogSections() => data.getDialogSections();

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            data.isSectionComplete = GetComponentInChildren<TextReveal>().revealComplete;

            if (gameObject.activeInHierarchy)
                data.canProgress = Input.GetMouseButtonDown(0);
           

        }
    }
}