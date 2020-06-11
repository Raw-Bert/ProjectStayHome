using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSections : MonoBehaviour
{
    List<string> sections;

    public void addDialogSection(string txt) => sections.Add(txt);
    public string getDialogSection(int index) => sections[index];
    public List<string> getDialogSections() => sections;

}