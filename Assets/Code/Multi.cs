using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Multi : MonoBehaviourPunCallbacks
{
    RoomOptions m_roomOptions = new RoomOptions();
    public GameObject m_testText;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        m_roomOptions.IsVisible = false;
        m_roomOptions.MaxPlayers = 2;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        PhotonNetwork.JoinOrCreateRoom("Test", m_roomOptions, TypedLobby.Default);
    }

    // Update is called once per frame
    void Update()
    {
        m_testText.GetComponent<TMP_Text>().text =
            "Number of players currently in the room : " + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room was created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room joined");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room join failed");
    }
}
