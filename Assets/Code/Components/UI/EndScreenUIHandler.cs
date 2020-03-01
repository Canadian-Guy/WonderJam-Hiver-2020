using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Handles all UI code calls in the end screen.
 */
public class EndScreenUIHandler : MonoBehaviour
{
    [Tooltip("The image displaying when the player won")]
    public  GameObject PlayerWon;

    [Tooltip("The image displaying when the player lost")]
    public GameObject PlayerLost;

    [Tooltip("The image displaying when the player are in a tie")]
    public GameObject PlayerTie;

    [Tooltip("The text displaying the scores acquired by player 1 during the game")]
    public TMP_Text P1ScoreText;

    [Tooltip("The text displaying the scores acquired by player 2 during the game")]
    public TMP_Text P2ScoreText;

    [Tooltip("Audioclip played when the player loses")]
    public AudioClip LoserSong;

    [Tooltip("Audioclip played when the player wins")]
    public AudioClip WinnerSong;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateResultImage(int p_result)
    {
        if (p_result == 1)
        {
            PlayerWon.SetActive(true);
            audioSource.clip = WinnerSong;
            audioSource.Play();
        }
        else if (p_result == 2)
        {
            PlayerLost.SetActive(true);
            audioSource.clip = LoserSong;
            audioSource.Play();
        }
        else
        {
            PlayerTie.SetActive(true);
        }
    }
 
    public void UpdateScoreText(int p_scoreOne, int p_scoreTwo)
    {
        P1ScoreText.text =  p_scoreOne + " pts";
        P2ScoreText.text =  p_scoreTwo + " pts";
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
