using UnityEngine;
using System.Collections.Generic;

/*
 * The configuration used by the word generator.
 * Holds all necessary information to make the generator work, but in a modular fashion.
 */
[CreateAssetMenu(menuName = "Words/Generator Configuration")]
public class WordGenConfig : ScriptableObject
{
    [Tooltip("The size of the generator's word dictionary, when it run out, it generates a new one of this size")]
    public int DictionarySize = 50;

    [Tooltip("The list of words available to the generator")]
    public List<WordWrapper> Words;

    public bool IsValid()
    {
        if(Words.Count == 0)
        {
            Debug.Log("There are no words in the word generator config!");
            return false;
        }

        return true;
    }
}