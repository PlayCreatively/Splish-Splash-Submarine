using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip clip;

    public void PlaySound(AudioClip clip)
    {
        var distFromCamera = Camera.main.transform.position - transform.position;

        // Only play sound if object is within the camera's view
        if(Mathf.Abs(distFromCamera.y) < Camera.main.orthographicSize 
        && Mathf.Abs(distFromCamera.x) < Camera.main.orthographicSize * Camera.main.aspect)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }

    public void PlaySound() => PlaySound(clip);
}
