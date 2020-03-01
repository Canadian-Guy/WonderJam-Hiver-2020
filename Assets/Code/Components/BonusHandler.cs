using UnityEngine;
using System.Collections;
using Photon.Pun;

/*
 * Acts as the interpreter for bonus word effects.
 * Applies everything required.
 */
public class BonusHandler : MonoBehaviour
{
    [Tooltip("This handler's photon view")]
    public PhotonView PView;

    [Tooltip("The spawn manager for the player")]
    public SpawnManager SpawnManager;

    [Tooltip("The score handler for the player")]
    public ScoreHandler ScoreHandler;

    [Tooltip("The active word set for the player")]
    public FallingWordSet ActiveWordsSet;

    public void FreezeActiveWords()
    {
        PView.RPC("FreezeWords", RpcTarget.All);
    }

    [PunRPC]
    private void FreezeWords()
    {
        foreach(FallingWord word in ActiveWordsSet._items)
            word.Falling.frozen = true;
    }

    public void ApplyMultSpeedToActiveWords(float p_multiplier)
    {
        PView.RPC("ApplyMultSpeedToWords", RpcTarget.All, p_multiplier);
    }

    [PunRPC]
    private void ApplyMultSpeedToWords(float p_multiplier)
    {
        foreach(FallingWord word in ActiveWordsSet._items)
            word.Falling.speed *= p_multiplier;
    }

    public void SpawnWords(int p_difficulty, params string[] p_words)
    {
        StartCoroutine(SpawnWordsDelayed(p_difficulty, p_words));
    }

    private IEnumerator SpawnWordsDelayed(int p_difficulty, string[] p_words)
    {
        for(int i = 0; i < p_words.Length; i++)
        {
            Word w = ScriptableObject.CreateInstance<Word>();

            w.Text = p_words[i];
            w.EventCode = 0;

            SpawnManager.SpawnWord(new WordWrapper() { Word = w, Difficulty = p_difficulty, Probability = 0 });

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void AddScore(int p_score)
    {
        ScoreHandler.PhotonIncreaseScore(p_score);
    }

    public void AddReverseWords(int p_amount)
    {
        SpawnManager.ReverseWordCount += p_amount;
    }

    public void AddFunctionWords(int p_amount)
    {
        SpawnManager.FunctionWordCount += p_amount;
    }

    public void AddCommentWords(int p_amount)
    {
        SpawnManager.CommentWordCount += p_amount;
    }

    public void AddSpeedUpWords(int p_amount)
    {
        SpawnManager.SpeedUpWordCount += p_amount;
    }

    public void AddSlowdownWords(int p_amount)
    {
        SpawnManager.SlowdownWordCount += p_amount;
    }
}
