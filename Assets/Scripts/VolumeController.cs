using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioManager audioManager;
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = GlobalSettings.Current.musicVolume;
        audioManager.OnVolumeChanged.AddListener(UpdateVolume);
    }

    void UpdateVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }
}


