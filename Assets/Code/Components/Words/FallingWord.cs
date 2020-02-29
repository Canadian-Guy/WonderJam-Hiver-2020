using UnityEngine;
using TMPro;

/*
 * This class represents a word falling on the screen.
 * It holds everything you might need to access about a word.
 */
[RequireComponent(typeof(TMP_Text), typeof(Falling)), DisallowMultipleComponent]
public class FallingWord : MonoBehaviour
{
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

    public void DestroyWord()
    {
        m_activeWordSet.Remove(this);
        Destroy(gameObject);
    }
}
