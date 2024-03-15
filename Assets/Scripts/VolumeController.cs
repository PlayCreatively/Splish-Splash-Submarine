using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VolumeController : MonoBehaviour
{
    private AudioManager audioManager;
    private AudioSource audioSource;
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioManager.OnVolumeChanged.AddListener(UpdateVolume);
    }
    void Start()
    {
        UpdateVolume(audioManager.Volume);
    }
    void UpdateVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }
}


