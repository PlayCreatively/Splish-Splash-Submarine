using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField]
    private float volume;
    public float Volume
    {
        get { return volume; }
        set
        {
            volume = value;
            GlobalSettings.Current.musicVolume = volume;
            OnVolumeChanged.Invoke(volume);
        }
    }
    public UnityEvent<float> OnVolumeChanged;

    void Awake()
    {
        volume = GlobalSettings.Current.musicVolume;
    }
}
