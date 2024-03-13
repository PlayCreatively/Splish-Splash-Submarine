using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenMusic : MonoBehaviour
{
    //public AudioClip musicClip;
    private AudioSource audioSource;
    public GameObject creditsMenu;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = musicClip;
    }

    void Update()
    {
        // Example condition to mute the music (replace with your own condition)
        if (creditsMenu.activeSelf)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute= false;
        }
    }
}
