using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InputHandler : MonoBehaviour
{
    public GameObjectSet activeCardsSet;
    public TMP_InputField inputField;

    private void Awake()
    {
        Clear();
    }


    public void ConfirmInput()
    {
        if (inputField.text == "") return; 

        Debug.Log("Sent this : *" + inputField.text + "*");


        Clear();
    }

    public void PATATE() { }

    private void Clear()
    {
        inputField.text = "";
    }
}
