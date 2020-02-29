using Photon.Pun;
using System.Collections;
using ExitGames.Client.Photon;
using Photon.Realtime;
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

    [Tooltip("Text component that displays the combo.")]
    public TMP_Text ComboText;
    public MyBox.OptionalInt maxCombo;
    public int comboPrerequisiteSupp = 3;
    public Animator comboAnimator;

    [Tooltip("Score increment tick speed, in seconds")]
    public float Speed = 0;

    [Tooltip("Score tick increments")]
    public int Increment = 0;

    public PhotonView photonView;

    private bool IsItTheEndOfTimes = false;

    private int m_combo = 1;
    private int _comboProgress = 0;

    void Start()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
        StartCoroutine(UpdateScore());

        ComboText.text = "x" + m_combo;
    }

    public void PhotonIncreaseScore(int p_scoreToAdd)
    {
        photonView.RPC("AddScore", RpcTarget.All, p_scoreToAdd);
        photonView.RPC("UpdateCombo", RpcTarget.All, false);
    }

    public void BreakCombo()
    {
        photonView.RPC("UpdateCombo", RpcTarget.All, true);
    }

    [PunRPC]
    public void UpdateCombo(bool p_break)
    {
        if (p_break)
        {
            if (m_combo > 1)
            {
                comboAnimator.SetTrigger("Shake");
                m_combo = 1;
            }

            _comboProgress = 0;

        }
        else if (!maxCombo.IsSet || m_combo < maxCombo.Value)
        {
            ++_comboProgress;

            if (_comboProgress >= m_combo + 1 + comboPrerequisiteSupp)
            {
                comboAnimator.SetTrigger("Pulse");
                _comboProgress = 0;
                m_combo++;
            }
        }

        ComboText.text = "x" + m_combo;
    }

    [PunRPC]
    public void AddScore(int p_score)
    {
        Score += (p_score * m_combo);
        
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
        PhotonIncreaseScore(5);
    }

    public void DEBUG_AddScoreCombo5()
    {
        PhotonIncreaseScore(25);
    }

    public void Attack()
    {
        byte evCode = 0;
        PhotonNetwork.RaiseEvent(evCode, photonView.ViewID, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
}
