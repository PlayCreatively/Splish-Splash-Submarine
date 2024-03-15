using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject creditsMenu;

    void Awake()
    {
        GlobalSettings.Current.musicVolume = 1;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
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
