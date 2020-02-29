using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InputHandler : MonoBehaviour
{
    public FallingWordSet activeWordSet;
    public TMP_InputField inputField;

    private void Awake()
    {
        Clear();
    }

    public void ConfirmInput()
    {
        if(inputField.text == "") return; 

        ManageActiveWords();

        Clear();
    }

    public void ManageActiveWords()
    {
        for(int i = activeWordSet.Count() - 1; i >= 0; i--)
        {
            FallingWord word = activeWordSet._items[i];

            if(!word) continue;

            if(word.Check(inputField.text)) word.DestroyWord();
        }
    }

    private void Clear()
    {
        inputField.text = "";
    }
}
