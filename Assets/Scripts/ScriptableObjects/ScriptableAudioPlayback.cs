using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Audio Playback", menuName = "Setting Objects/Audio Player")]
public class ScriptableAudioPlayback : ScriptableObject
{
    const float MIN_PITCH = .01f;

    public AudioMixerGroup mixerGroup;
    public AudioClip clip;

    [Range(-50, 0)]
    [SerializeField]
    private float dbVolume = 0;
    public float DbVolume
    {
        get { return dbVolume; }
        set
        {
            Debug.Log("Setting volume to " + value);
            dbVolume = value;
            // convert db to linear
            volume = Mathf.Pow(10, dbVolume / 20);
        }
    }
    [Range(MIN_PITCH, 3)]
    public float pitch = 1;
    float volume = 1;
    AudioSource loopingAudioSource;
    
    [Range(0f, .5f)]
    public float randomPitchVariance = .1f;
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
    public void Loop()
    {
        ApplyLoopVolume();
        var temp = new GameObject("temp_audio_" + name);
        loopingAudioSource = temp.AddComponent<AudioSource>();
        loopingAudioSource.outputAudioMixerGroup = mixerGroup;
        loopingAudioSource.clip = clip;
        loopingAudioSource.pitch = pitch + Random.Range(-randomPitchVariance, randomPitchVariance);
        loopingAudioSource.pitch = Mathf.Max(MIN_PITCH, loopingAudioSource.pitch);
        loopingAudioSource.volume = volume;
        loopingAudioSource.loop = true;
        loopingAudioSource.Play();
    }
    public void Stop()
    {
        loopingAudioSource.Stop();
        Destroy(loopingAudioSource.gameObject);
    }
    public void UpdateLoopVolume()
    {
        GlobalSettings.Current.musicVolume = volume;
        loopingAudioSource.volume = volume;
    }
    private void ApplyLoopVolume()
    {
        volume = GlobalSettings.Current.musicVolume;
    }
}