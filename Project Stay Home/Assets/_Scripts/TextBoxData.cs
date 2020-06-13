using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextBoxSystem
{
    public class TextBoxData : MonoBehaviour
    {
        int m_counter = 0;
        List<string> sections = new List<string>();
        [HideInInspector] public Vector2 position = Vector2.zero;
        [HideInInspector] public bool isSectionComplete = true;
        [HideInInspector] public bool canProgress = false;

        public void initData(TextBoxData data)
        {
            m_counter = data.m_counter;
            sections = data.sections;
            position = data.position;
        }
        public int getCounter() => m_counter;
        public void increaseCounter() => ++m_counter;
        public void decreaseCounter() => --m_counter;

        public void addDialogSection(string txt) => sections.Add(txt);
        public string getDialogSection() => sections[m_counter];
        public string getDialogSection(int index) => sections[index];
        public List<string> getDialogSections() => sections;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (gameObject.activeInHierarchy)
                if (Input.GetMouseButtonDown(0))
                {
                    canProgress = true;
                }

        }
    }
}