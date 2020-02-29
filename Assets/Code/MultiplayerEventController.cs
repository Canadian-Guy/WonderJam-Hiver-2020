using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerEventController : MonoBehaviourPunCallbacks, IOnEventCallback
{

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        Debug.Log("Received Event");
        if (eventCode == 0)
        {
            PhotonNetwork.LoadLevel("TestScore");
        }
    }

}
