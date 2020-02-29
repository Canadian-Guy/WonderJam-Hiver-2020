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

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        var attackingPlayer = photonEvent.CustomData;
        Debug.Log("Received Event");
        if (eventCode == 0)
        {
            Debug.Log("Event 0 received");
            foreach (var player in PhotonNetwork.PhotonViews)
            {
                if (player.ViewID.Equals(photonEvent.Sender))
                {
                    player.gameObject.GetComponent<ScoreHandler>().PhotonIncreaseScore(-5);
                }
            }
            
        }
    }
}