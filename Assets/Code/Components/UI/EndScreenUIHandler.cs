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
    public Image PlayerWon;

    [Tooltip("The image displaying when the player lost")]
    public Image PlayerLost;

    [Tooltip("The image displaying when the player are in a tie")]
    public Image PlayerTie;

    [Tooltip("The text displaying the scores acquired by player 1 during the game")]
    public TMP_Text P1ScoreText;

    [Tooltip("The text displaying the scores acquired by player 2 during the game")]
    public TMP_Text P2ScoreText;

    public void UpdateResultImage(int p_result)
    {
        if (p_result == 1)
        {
            PlayerWon.enabled = true;
        }
        else if (p_result == 2)
        {
            PlayerLost.enabled = true;
        }
        else
        {
            PlayerTie.enabled = true;
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
