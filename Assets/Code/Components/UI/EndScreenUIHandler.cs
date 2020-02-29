using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/*
 * Handles all UI code calls in the end screen.
 */
public class EndScreenUIHandler : MonoBehaviour
{
    [Tooltip("The header displaying whether the player won or lost")]
    public TMP_Text ResultHeader;

    [Tooltip("The text displaying the scores acquired by both players during the game")]
    public TMP_Text ScoreText;

    public void UpdateResultHeader(int p_result)
    {
        ResultHeader.text = "You " + (p_result == 1 ? "won" : (p_result == 2 ? "lost" : "tied")) + "!";
    }

    public void UpdateScoreText(int p_scoreOne, int p_scoreTwo)
    {
        ScoreText.text = p_scoreOne + " - " + p_scoreTwo;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
