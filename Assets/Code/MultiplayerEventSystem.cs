using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using JetBrains.Annotations;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MultiplayerEventSystem : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public InputHandler[] Players = new InputHandler[2];

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.CustomData == null)
            return;
        int eventCode = (int)photonEvent.Code;
        int attackingPlayer = (int)photonEvent.CustomData;
        Debug.Log("Received Event : " + eventCode);
        if (eventCode == 0)
        {
            Debug.Log("Event 0 received");
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.ActorNumber != attackingPlayer)
                {
                    Players[player.ActorNumber].ScoreHandler.PhotonIncreaseScore(-50000);
                }
            }
            
        }
    }
}