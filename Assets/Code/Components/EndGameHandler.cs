using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/*
 * Class handling the end of game transition.
 */
public class EndGameHandler : MonoBehaviour
{
    [Tooltip("The score handler for player one")]
    public ScoreHandler PlayerOneScore;

    [Tooltip("The score handler for player two")]
    public ScoreHandler PlayerTwoScore;

    private int m_playerOneScore;
    private int m_playerTwoScore;
    private int m_winnerID;
    private int m_playerID;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnLevelLoad;
    }

    public void EndGame()
    {
        m_playerOneScore = PlayerOneScore.Score;
        m_playerTwoScore = PlayerTwoScore.Score;
        m_winnerID = m_playerOneScore > m_playerTwoScore ? 1 : 2;
        m_playerID = PhotonNetwork.LocalPlayer.ActorNumber;

        if(m_playerOneScore == m_playerTwoScore) m_winnerID = -1;

        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(2);
    }

    public void OnLevelLoad(Scene p_scene, LoadSceneMode p_loadSceneMode)
    {
        if(p_scene.buildIndex == 0)
        {
            LocalDestroy();
            return;
        }

        GameObject canvas = GameObject.Find("Canvas");

        if(canvas != null)
        {
            EndScreenUIHandler uiHandler = canvas.GetComponent<EndScreenUIHandler>();

            if(uiHandler)
            {
                uiHandler.UpdateResultHeader(m_winnerID == m_playerID ? 1 : (m_winnerID == -1 ? 0 : 2));
                uiHandler.UpdateScoreText(m_playerOneScore, m_playerTwoScore);

                LocalDestroy();
            }
        }
    }

    private void LocalDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelLoad;
        Destroy(gameObject);
    }
}
