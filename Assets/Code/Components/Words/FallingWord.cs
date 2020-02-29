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

    [HideInInspector] public WordWrapper Wrapper;
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
        return Wrapper.Word && Wrapper.Word.Text == p_string;
    }

    public void Init(WordWrapper p_word, FallingWordSet p_activeWordSet)
    {
        m_activeWordSet = p_activeWordSet;
        Wrapper = p_word;
        Text.text = Wrapper.Word.Text;
    }

    public int GetScore()
    {
        return Wrapper.Difficulty;
    }

    public void DestroyWord(bool p_breaksCombo)
    {
        BrokeCombo = p_breaksCombo;
        RequiresDeletion = true;
    }
}
