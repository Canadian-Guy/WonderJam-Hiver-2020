using System.IO;
using UnityEngine;

/*
 * This represents a word and all the specifics about it.
 * This is essentially the data container for the words that will appear on screen.
 */
[CreateAssetMenu(menuName = "Words/Word")]
public class Word : ScriptableObject
{
    [Tooltip("The text representing the word ; what this word says")]
    public string Text;

    [Tooltip("The amount of points this word is worth")]
    public int Points;
}

/*
 * This class wraps a word with its probability and difficulty to allow for
 * better (and standardized) parametrization in the editor
 */
[System.Serializable]
public struct WordWrapper
{
    [Tooltip("The word to which the difficulty and probability apply")]
    public Word Word;

    [Tooltip("The word's difficulty, how difficult to type it is")]
    [Range(0, 10)] public int Difficulty;

    [Tooltip("The word's probability to be picked")]
    [Range(0, 100)] public float Probability;
}