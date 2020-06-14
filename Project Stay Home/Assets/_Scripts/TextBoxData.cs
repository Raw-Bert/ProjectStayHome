using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TextBoxSystem
{
    public enum TextBoxType
    {
        Narration,
        Dialog,
        Internal
    }
    public class TextBoxData
    {
        List<string> sections { get; } = new List<string>();
        public string name { get; set; }
        public TextBoxType textBoxType { get; set; }
        public List<MovementType> movement { get; set; } = new List<MovementType>();
        public Vector2 position { get; set; } = Vector2.zero;
        public bool isSectionComplete { get; set; } = false;
        public bool canProgress { get; set; } = false;

        public void addDialogSection(string txt) => sections.Add(txt);

        public string getDialogSection(int index) => sections[index];
        public List<string> getDialogSections() => sections;
    }
}