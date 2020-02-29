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

    [Tooltip("Score increment tick speed, in seconds")]
    public float Speed = 0;

    [Tooltip("Score tick increments")]
    public int Increment = 0;

    private PhotonView photonView;

    private static int Cmpt = 1;

    private bool IsItTheEndOfTimes = false;

    private int m_combo = 1;

    void Start()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
        photonView = gameObject.AddComponent<PhotonView>();
        photonView.ViewID = Cmpt++;
        StartCoroutine(UpdateScore());
    }

    public void PhotonIncreaseScore(int p_scoreToAdd)
    {
        photonView.RPC("AddScore", RpcTarget.All,p_scoreToAdd);
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
