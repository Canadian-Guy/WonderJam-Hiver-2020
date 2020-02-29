using System.Collections;
using UnityEngine;
using Photon.Pun;

/*
 * Detects combo breaks and deletes words that require deletion.
 */
public class WordCleaner : MonoBehaviour
{
    [Tooltip("The score handler linked to this combo break updater")]
    public ScoreHandler Score;

    [Tooltip("The active set of words on screen")]
    public FallingWordSet ActiveWordSet;

    [Tooltip("The player ID linked to this cleaner")]
    public int ValidPlayerID;

    void Awake()
    {
        StartCoroutine(UpdateWords());
    }

    private IEnumerator UpdateWords()
    {
        while(PhotonNetwork.InRoom)
        {
            yield return new WaitForSeconds(0.25f);

            for(int i = ActiveWordSet.Count() - 1; i >= 0; i--)
            {
                FallingWord word = ActiveWordSet._items[i];

                if(!word) continue;

                if(word.BrokeCombo && PhotonNetwork.LocalPlayer.ActorNumber == ValidPlayerID) 
                    Score.BreakCombo();

                if(word.RequiresDeletion)
                {
                    ActiveWordSet.Remove(word);
                    Destroy(word.gameObject);
                }
            }
        }
    }
}
