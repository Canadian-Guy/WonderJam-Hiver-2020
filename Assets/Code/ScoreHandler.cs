using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [Tooltip("This is the current score!")]
    public int Score = 0;
    [Tooltip("Text component that displays the score.")]
    public TMP_Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int p_score, int p_combo = 1)
    {
        Score += (p_score * p_combo);
        ScoreText.text = Score.ToString();
    }

    public void DEBUG_AddScoreCombo0()
    {
        AddScore(5);
    }

    public void DEBUG_AddScoreCombo5()
    {
        AddScore(5, 5);
    }
}
