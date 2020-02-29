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

        ManageActiveWords();

        Clear();
    }

    public void ManageActiveWords()
    {
        SpellChecker sc;
        for(int i = activeCardsSet.Count() - 1; i >= 0; i--)
        {
            sc = activeCardsSet._items[i].GetComponent<SpellChecker>();

            if (sc && sc.Check(inputField.text))
            {
                activeCardsSet.Remove(sc.gameObject);
                Destroy(sc.gameObject);
            }
        }
    }

    private void Clear()
    {
        inputField.text = "";
    }
}
