using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerConnector : MonoBehaviourPunCallbacks
{
    RoomOptions m_roomOptions = new RoomOptions();
    public MainMenuUIHandler MainMenuHandler;
    public Timer Timer;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        m_roomOptions.IsVisible = true;
        m_roomOptions.MaxPlayers = 2;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Lobby Joined");
    }

    public void JoinRoom(string p_room)
    {
        PhotonNetwork.JoinOrCreateRoom(p_room.ToLower(), m_roomOptions, TypedLobby.Default);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
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
        Debug.Log(PhotonNetwork.CurrentRoom.Name);

        MainMenuHandler.RoomJoined();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room join failed");

        MainMenuHandler.RoomFail();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.CurrentRoom.EmptyRoomTtl = 0;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenuHandler.forceBackToSelectRoom();
            return;
        }
        base.OnLeftRoom();
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(Timer && Timer.CurrentTime <= 1.0f) return;
        if(PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();

        SceneManager.LoadScene(0);
    }
}
