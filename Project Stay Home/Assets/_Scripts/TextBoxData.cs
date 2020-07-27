using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        ///<summary>
        /// The name of the character
        ///</summary>
        public string name { get; set; }

        ///<summary>
        /// Text box type to desplay (i.e. Narration, Dialog, Internal)
        ///</summary>
        public TextBoxType textBoxType { get; set; } = TextBoxType.Dialog;

        ///<summary>
        /// changes how each section
        ///</summary>
        public List<MovementType> movement { get; private set; } = new List<MovementType>();
        public Vector2 position { get; set; } = Vector2.zero;
        public bool isSectionComplete { get; set; } = false;
        public bool canProgress { get; set; } = false;
        List<string> sections { get; } = new List<string>();

        ///<summary>
        /// set how the text moves at different sections 
        ///</summary>
        public void setMovement(int index, MovementType move)
        {

            if (index >= 0 && index < movement.Count)
            {
                movement[index] = move;
            }
            else if (index >= 0)
            {
                movement.AddRange(Enumerable.Repeat(MovementType.None, index+1 - movement.Count));
                movement[index] = move;
            }

        }

        ///<summary>
        /// Adds a new dialog section that will only appear after the last section is compleat
        ///</summary>
        public void addDialogSection(string txt) => sections.Add(txt);

        ///<summary>
        /// Gets the dialog section at specified index.
        /// If index is out of range string == null
        ///</summary>
        public string getDialogSection(int index) => sections[index] ?? null;

        ///<summary>
        /// Gets a list of all dialog sections
        ///</summary>
        public List<string> getDialogSections() => sections;
    }
}