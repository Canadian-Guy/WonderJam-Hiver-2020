using System.Collections;
using UnityEngine;
using MyBox;
using Photon.Pun;
using TMPro;

/*
 * Handles spawning words on screen in different locations at a specific speed
 */
public class SpawnManager : MonoBehaviour
{
    [Tooltip("The photon view for this spawner")]
    public PhotonView PView;

    [Tooltip("The transform within which words can spawn")]
    public RectTransform SpawnZone;

    [Tooltip("Whether words always spawn in the middle or spawn randomly around the transform")]
    public bool RandomizeLocations;

    [Tooltip("The dictionary responsible for the word generation used by this spawn manager")]
    public WordGenerator Dictionary;

    [Tooltip("Time it takes for the game to end")]
    [Range(0f, 500f)] public float GameLength;

    [Tooltip("Whether or not words spawn more often as the game goes on")]
    public bool SpeedUpSpawns;

    [Tooltip("The spawn rate range (in seconds used to spawn words, will go from upper to lower bound over the game")]
    [MinMaxRange(0f, 15f)] public RangedFloat SpawnRateRange;

    [Tooltip("Whether or not words fall faster as the game goes on")]
    public bool SpeedUpWords = true;

    [Tooltip("The speed range at which the words fall at, will go from lower to upper bound over the game")]
    [MinMaxRange(0f, 15f)] public RangedFloat SpeedRange;

    private float m_spawnRate;
    private float m_speed;

    void Start()
    {
        m_speed = SpeedRange.Min;
        m_spawnRate = SpawnRateRange.Max;

        StartCoroutine(UpdateVariables());
        StartCoroutine(SpawnWordRoutine());
    }

    private IEnumerator UpdateVariables()
    {
        while(PhotonNetwork.InRoom && ((SpeedUpWords && m_speed < SpeedRange.Max) || 
                                       (SpeedUpSpawns && m_spawnRate > SpawnRateRange.Min)))
        {
            yield return new WaitForSeconds(0.25f);

            if(SpeedUpWords) m_speed += (SpeedRange.Max - SpeedRange.Min) / (GameLength * 4);
            if(SpeedUpSpawns) m_spawnRate -= (SpawnRateRange.Max - SpawnRateRange.Min) / (GameLength * 4);
        }
    }

    private IEnumerator SpawnWordRoutine()
    {
        while(PhotonNetwork.InRoom)
        {
            yield return new WaitForSeconds(m_spawnRate);

            WordWrapper generated = Dictionary.FetchWord();
            float randomPosition = Random.Range(0f, 100f);

            PView.RPC("SpawnWord", RpcTarget.All, generated.Word.Text, generated.Difficulty, randomPosition);
        }
    }

    [PunRPC]
    private void SpawnWord(string p_word, int p_difficulty, float p_randomPosition)
    {
        Word w = ScriptableObject.CreateInstance<Word>();
        w.Text = p_word;

        WordWrapper ww = new WordWrapper() { Word = w, Difficulty = p_difficulty, Probability = 0 };
        GameObject word = Dictionary.InstantiateWord(ww);
        FallingWord fWord = word.GetComponent<FallingWord>();

        fWord.Falling.speed = m_speed;

        float wordLength = fWord.Text.preferredWidth;
        float randomMovement = RandomizeLocations ? (SpawnZone.rect.width - wordLength) * (p_randomPosition / 100f) : 0f;

        if(randomMovement > (SpawnZone.rect.width - wordLength) / 2)
            randomMovement = (SpawnZone.rect.width - wordLength) / 2 - randomMovement;

        word.transform.localPosition = new Vector3(SpawnZone.localPosition.x + randomMovement,
                                                   SpawnZone.localPosition.y);
    }
}
