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
        label.text = "MISSING WORD";
    }

    public bool Check(string p_string)
    {
        return word && word.Text == p_string;
    }

    public void Init(Word p_word)
    {
        word = p_word;
        label.text = p_word.Text;
    }
}
