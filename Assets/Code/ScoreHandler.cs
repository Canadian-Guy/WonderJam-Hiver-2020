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
    [Tooltip("The player ID linked to this handler")]
    public int ValidPlayerID;

    [Tooltip("This is the current score!")]
    public ParticleSystem particlesystem;

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

    [Tooltip("Sound to play when max combo is acheived")]
    public AudioClip FrenzyClip;
    private AudioSource _audioSource;

    public PhotonView photonView;

    private bool IsItTheEndOfTimes = false;

    public int Combo { get; private set; } = 1;

    private int _comboProgress = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
        StartCoroutine(UpdateScore());

        ComboText.text = "x" + Combo;

        particlesystem.Stop();
    }

    public void PhotonIncreaseScore(int p_scoreToAdd)
    {
        photonView.RPC("UpdateCombo", RpcTarget.All, false);
        photonView.RPC("AddScore", RpcTarget.All, p_scoreToAdd);
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
            if (Combo > 1)
            {
                particlesystem.Stop();

                if (maxCombo.IsSet && Combo == maxCombo.Value)
                    comboAnimator.SetTrigger("ExitFrenzy");
                else
                    comboAnimator.SetTrigger("Shake");

                --Combo;
            }

            _comboProgress = 0;

        }
        else if (!maxCombo.IsSet || Combo < maxCombo.Value)
        {

            particlesystem.Stop();

            ++_comboProgress;

            if (_comboProgress >= Combo + 1 + comboPrerequisiteSupp)
            {
                _comboProgress = 0;
                ++Combo;

                if (Combo == maxCombo.Value)
                {
                    comboAnimator.SetTrigger("EnterFrenzy");

                    if (PhotonNetwork.LocalPlayer.ActorNumber == ValidPlayerID)
                        _audioSource.PlayOneShot(FrenzyClip);
                }
                else
                    comboAnimator.SetTrigger("Pulse");
            }
        }

        if (Combo == maxCombo.Value)
        {
            ComboText.text = "FRENZY";

            particlesystem.Play();
        }
        else
        {
            ComboText.text = "x" + Combo;
        }
    }

    [PunRPC]
    public void AddScore(int p_score)
    {
        Score += (p_score * Combo);
        
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
