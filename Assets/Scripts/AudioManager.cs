using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip[] audio_Clips;
    public AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayAudio(int number)
    {
        audioSource.clip = audio_Clips[number];
        audioSource.Play();
    }
}