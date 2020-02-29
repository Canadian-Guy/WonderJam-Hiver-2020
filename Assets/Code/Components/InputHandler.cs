using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class InputHandler : MonoBehaviour
{
    public FallingWordSet activeWordSet;
    public TMP_InputField inputField;
    public ScoreHandler ScoreHandler;
    public PhotonView PView;
    public int ValidPlayerID;

    private void Awake()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if(actorNumber != ValidPlayerID) inputField.interactable = false;
        else if(EventSystem.current)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            GetComponent<Selectable>().OnSelect(null);
        }

        Clear();
    }

    public void ConfirmInput()
    {
        if(inputField.text == "") return; 

        ManageActiveWords();

        Clear();
    }

    public void ManageActiveWords()
    {
        PView.RPC("TypedWord", RpcTarget.All, inputField.text);
    }

    [PunRPC]
    private void TypedWord(string p_input)
    {
        for(int i = activeWordSet.Count() - 1; i >= 0; i--)
        {
            FallingWord word = activeWordSet._items[i];

            if(!word) continue;

            if(word.Check(p_input))
            {
                if (word.tag == "BonusWord")
                {
                    ScoreHandler.PhotonIncreaseScore(word.GetScore());
                    PhotonNetwork.RaiseEvent(0, PhotonNetwork.LocalPlayer, RaiseEventOptions.Default, SendOptions.SendReliable);
                }
                else if(PhotonNetwork.LocalPlayer.ActorNumber == ValidPlayerID)
                    ScoreHandler.PhotonIncreaseScore(word.GetScore());

                word.DestroyWord(false);
            }
        }
    }

    private void Clear()
    {
        inputField.text = "";
    }
}
