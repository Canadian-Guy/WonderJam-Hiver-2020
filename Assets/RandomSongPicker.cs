using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSongPicker : MonoBehaviour
{
    [Tooltip("Put all the songs in here, one will be picked!")]
    public List<AudioClip> clips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, clips.Count);
        audioSource.clip = clips[random];
        audioSource.Play();
    }
}
