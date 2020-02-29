using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerConnector : MonoBehaviourPunCallbacks
{
    RoomOptions m_roomOptions = new RoomOptions();
    public MainMenuUIHandler MainMenuHandler;
    public InputField RoomInputField;
    private byte evCode = 0;

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
    }

    public void JoinRoom(string p_room)
    {
        PhotonNetwork.JoinOrCreateRoom(p_room.ToLower(), m_roomOptions, TypedLobby.Default);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PhotonNetwork.RaiseEvent(evCode, null, new RaiseEventOptions(){Receivers = ReceiverGroup.All}, new SendOptions(){Reliability = true});
        }
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
}
