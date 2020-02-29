using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This represents the word generator, it spawns words depending on a variety of variables
 * using a generator configuration (WordGenConfig) to know what to generate.
 */
public class WordGenerator : MonoBehaviour
{
    [Tooltip("This generator's configuration values. Will update the generator if hotswappped")]
    public WordGenConfig GeneratorConfiguration;

    public FallingWordSet activeWordSet;
    public GameObject wordPrefab;
    public Transform spawnParent;

    private WordGenConfig m_genConfig;
    private int CurrentDifficulty;

    [SerializeField] private List<WordWrapper> m_wordDictionary;

    void Start()
    {
        CurrentDifficulty = 1;
        m_wordDictionary = new List<WordWrapper>();
        m_genConfig = GeneratorConfiguration;

        Generate();

        StartCoroutine(IncrementDifficulty());
    }

    void Update()
    {
        if(m_genConfig != GeneratorConfiguration)
        {
            m_genConfig = GeneratorConfiguration;

            Generate();
        }
    }

    private IEnumerator IncrementDifficulty()
    {
        while(true) // TODO: change this to not run until it hits diff 10
        {
            yield return new WaitForSeconds(m_genConfig.TimeToReachDiffCap / 9f);

            CurrentDifficulty++;

            Generate();

            if(CurrentDifficulty == 10) break;
        }
    }

    public GameObject CreateWord()
    {
        if(m_wordDictionary.Count == 0) Generate();

        int selectedIndex = Random.Range(0, m_wordDictionary.Count);
        WordWrapper selected = m_wordDictionary[selectedIndex];

        m_wordDictionary.RemoveAt(selectedIndex);

        GameObject go = Instantiate(wordPrefab, spawnParent);
        FallingWord word = go.GetComponent<FallingWord>();

        if(word)
        {
            word.Init(selected, activeWordSet);
            activeWordSet.Add(word);
        }

        return go;
    }

    private void Generate()
    {
        if(m_genConfig && m_genConfig.IsValid())
        {
            Debug.Log("Generating word dictionary at difficulty " + CurrentDifficulty + "...");

            m_wordDictionary.Clear();

            List<WordWrapper> availableWords = new List<WordWrapper>();

            foreach(WordWrapper ww in m_genConfig.Words)
                if(ww.Difficulty <= CurrentDifficulty)
                    availableWords.Add(ww);

            float totalProbability = 0;

            foreach(WordWrapper ww in availableWords)
                totalProbability += ww.Probability;

            float probabilityScale = 100f / totalProbability;

            foreach(WordWrapper ww in availableWords)
                for(int i = 0; i < Mathf.RoundToInt(m_genConfig.DictionarySize * (ww.Probability * probabilityScale / 100f)); i++)
                    m_wordDictionary.Add(ww);

            Debug.Log("Generated a " + m_wordDictionary.Count + " word dictionary with " + 
                      availableWords.Count + " different words!");
        }
    }
}