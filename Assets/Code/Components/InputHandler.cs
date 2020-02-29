using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    public FallingWordSet activeWordSet;
    public TMP_InputField inputField;
    public ScoreHandler ScoreHandler;
    public PhotonView PView;
    public int ValidPlayerID;

    [Tooltip("Sound played when a valid word is typed")]
    public AudioClip PopSound;
    
    [Tooltip("Sound played when a valid word is typed")]
    public AudioClip KeystrokeSound;

    [Tooltip("Sound played when an invalid word is typed")]
    public AudioClip InvalidSound;

    private AudioSource audioSource;
    private void Awake()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if(actorNumber != ValidPlayerID) inputField.interactable = false;
        else if(EventSystem.current)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            GetComponent<Selectable>().OnSelect(null);
        }

        audioSource = GetComponent<AudioSource>();

        Clear();
    }

    public void PlayKeyStroke()
    {
        audioSource.PlayOneShot(KeystrokeSound);
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
        bool m_foundWord = false;
        for(int i = activeWordSet.Count() - 1; i >= 0; i--)
        {
            FallingWord word = activeWordSet._items[i];

            if(!word) continue;

            if(word.Check(p_input))
            {
                if(PhotonNetwork.LocalPlayer.ActorNumber == ValidPlayerID)
                {
                    m_foundWord = true;
                    ScoreHandler.PhotonIncreaseScore(word.GetScore());
                    audioSource.PlayOneShot(PopSound);
                }

                word.DestroyWord(false);
            }
        }

        if(PhotonNetwork.LocalPlayer.ActorNumber == ValidPlayerID && !m_foundWord)
        {
            audioSource.PlayOneShot(InvalidSound);
        }
    }

    private void Clear()
    {
        inputField.text = "";
    }
}
