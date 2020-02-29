using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    private int player1Score;
    private int player2Score;
    public GameObject player1ScoreText;
    public GameObject player2ScoreText;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        player1ScoreText.GetComponent<TMP_Text>().text = player1Score.ToString();
        player2ScoreText.GetComponent<TMP_Text>().text = player2Score.ToString();
        photonView = gameObject.AddComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PhotonIncreasePlayer1Score()
    {
        photonView.RPC("IncreasePlayer1Score", RpcTarget.All);
    }

    public void PhotonIncreasePlayer2Score()
    {
        photonView.RPC("IncreasePlayer2Score", RpcTarget.All);
    }

    [PunRPC]
    public void IncreasePlayer1Score()
    {
        player1Score++;
        player1ScoreText.GetComponent<TMP_Text>().text = player1Score.ToString();

    }

    [PunRPC]
    public void IncreasePlayer2Score()
    {
        player2Score++;
        player2ScoreText.GetComponent<TMP_Text>().text = player2Score.ToString();
    }
}
