using UnityEngine;

public class PlayDeathSound : MonoBehaviour
{
    // The audio source component
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        Debug.Log("Playing death sound");
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
