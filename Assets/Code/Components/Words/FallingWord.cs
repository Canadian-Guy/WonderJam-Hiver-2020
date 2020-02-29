using UnityEngine;
using TMPro;
using Photon.Pun;

/*
 * This class represents a word falling on the screen.
 * It holds everything you might need to access about a word.
 */
[RequireComponent(typeof(TMP_Text), typeof(Falling)), DisallowMultipleComponent]
public class FallingWord : MonoBehaviour
{
    [HideInInspector] public bool BrokeCombo; // always false unless the combo breaks with this word
    [HideInInspector] public bool RequiresDeletion;

    [HideInInspector] public WordWrapper Word;
    [HideInInspector] public TMP_Text Text;
    [HideInInspector] public Falling Falling;

    private FallingWordSet m_activeWordSet;

    void Awake()
    {
        Text = GetComponent<TMP_Text>();
        Falling = GetComponent<Falling>();

        Text.text = "MISSING WORD";
    }

    public bool Check(string p_string)
    {
        return Word.Word && Word.Word.Text == p_string;
    }

    public void Init(WordWrapper p_word, FallingWordSet p_activeWordSet)
    {
        m_activeWordSet = p_activeWordSet;
        Word = p_word;
        Text.text = Word.Word.Text;
    }

    public int GetScore()
    {
        return Word.Difficulty;
    }

    public void DestroyWord(bool p_breaksCombo)
    {
        BrokeCombo = p_breaksCombo;
        RequiresDeletion = true;
    }
}
