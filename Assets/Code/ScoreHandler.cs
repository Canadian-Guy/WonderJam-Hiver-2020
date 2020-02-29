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

    [Tooltip("Score increment tick speed, in seconds")]
    public float Speed = 0;

    [Tooltip("Score tick increments")]
    public int Increment = 0;

    public PhotonView photonView;

    private bool IsItTheEndOfTimes = false;

    private int m_combo = 1;

    void Start()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
        StartCoroutine(UpdateScore());
    }

    public void PhotonIncreaseScore(int p_scoreToAdd)
    {
        photonView.RPC("AddScore", RpcTarget.All,p_scoreToAdd);
        photonView.RPC("UpdateCombo", RpcTarget.All, m_combo + 1);
    }

    public void BreakCombo()
    {
        photonView.RPC("UpdateCombo", RpcTarget.All, 1);
    }

    [PunRPC]
    public void UpdateCombo(int p_combo)
    {
        m_combo = p_combo;

        ComboText.text = m_combo + "x";
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

    public void Attack()
    {
        byte evCode = 0;
        PhotonNetwork.RaiseEvent(evCode, photonView.ViewID, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
}
