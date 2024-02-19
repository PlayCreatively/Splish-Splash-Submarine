using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Audio Playback", menuName = "Setting Objects/Audio Player")]
public class ScriptableAudioPlayback : ScriptableObject
{
    const float MIN_PITCH = .01f;

    public AudioMixerGroup mixerGroup;
    public AudioClip clip;

    [Range(-50, 0)]
    public float dbVolume = 0;
    [Range(MIN_PITCH, 3)]
    public float pitch = 1;
    float volume = 1;
    
    [Range(0f, .5f)]
    public float randomPitchVariance = .1f;

    void OnValidate()
    {
        // convert db to linear
        volume = Mathf.Pow(10, dbVolume / 20);
    }

    void OnEnable()
    {
        OnValidate();
    }

    public void Play()
    {
        var temp = new GameObject("temp_audio_"+name);
        var audioSource = temp.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.clip = clip;
        audioSource.pitch = pitch + Random.Range(-randomPitchVariance, randomPitchVariance);
        audioSource.pitch = Mathf.Max(MIN_PITCH, audioSource.pitch);
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(temp, clip.length / audioSource.pitch);
    }

   
}