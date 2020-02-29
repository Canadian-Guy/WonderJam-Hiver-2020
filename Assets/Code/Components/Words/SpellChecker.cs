using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellChecker : MonoBehaviour
{
    public TMP_Text label;
    public Word word;

    private void Awake()
    {
        if (word)
            label.text = word ? word.Text : "???";
    }

    public void Check(string p_string)
    {
        if (word && word.Text == p_string)
            Destroy(gameObject);
    }
}
