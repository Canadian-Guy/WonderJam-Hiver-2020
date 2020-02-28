using System.Collections;
using TMPro;
using UnityEngine;
/*
 * Simple class that handles score stuff
 * (Adding, displaying, updating the display)
 */
public class ScoreHandler : MonoBehaviour
{
    [Tooltip("This is the current score!")]
    public int Score = 0;
    [Tooltip("Text component that displays the score.")]
    public TMP_Text ScoreText;
    [Tooltip("Score increment tick speed, in seconds")]
    public float Speed = 0;
    [Tooltip("Score tick increments")]
    public int Increment = 0;
    private bool IsItTheEndOfTimes = false;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
        StartCoroutine(UpdateScore());
    }

    public void AddScore(int p_score, int p_combo = 1)
    {
        Score += (p_score * p_combo);
        //ScoreText.text = Score.ToString();
    }

    public void Reset()
    {
        //Reset le score
        Score = 0;
        //Reset le UI
        ScoreText.text = "0";
        //Reset la coroutine
        StopCoroutine(UpdateScore());
        StartCoroutine(UpdateScore());
    }

    IEnumerator UpdateScore()
    {
        //As long as it is not the end of times...
        while(!IsItTheEndOfTimes) {
            if(Score.ToString() != ScoreText.text)
            {
                //tmp score est le score affiché
                int m_tmpScore = int.Parse(ScoreText.text);
                m_tmpScore += Increment;
                if(m_tmpScore > Score)
                {
                    //Si on depasse le score, on a merdé et on devient égal au score.
                    m_tmpScore = Score;
                }
                ScoreText.text = m_tmpScore.ToString();
            }
            yield return new WaitForSeconds(Speed);
        }
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
