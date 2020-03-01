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

    public WordWrapper Wrapper;
    [HideInInspector] public TMP_Text Text;
    [HideInInspector] public Falling Falling;

    public GameObject popUpPrefab;

    private FallingWordSet m_activeWordSet;

    void Awake()
    {
        Text = GetComponent<TMP_Text>();
        Falling = GetComponent<Falling>();

        Text.text = "MISSING WORD";
    }

    public void CreatePopUp(int p_combo, Transform p_parent)
    {
        GameObject pop = Instantiate(popUpPrefab, p_parent);
        PopUpHandler handler = pop.GetComponent<PopUpHandler>();

        if (handler)
            handler.SetText(Wrapper.Word.Points * p_combo);

        pop.transform.position = transform.position;
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

    public void ChangeWord(Word p_word)
    {
        Wrapper.Word = p_word;
        Text.text = Wrapper.Word.Text;
    }

    public int GetScore()
    {
        Debug.Log(Wrapper.Word.Points);
        return Wrapper.Word.Points;
    }

    public void DestroyWord(bool p_breaksCombo)
    {
        BrokeCombo = p_breaksCombo;
        RequiresDeletion = true;
    }
}
