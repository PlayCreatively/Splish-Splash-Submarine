using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public ScriptableAudioPlayback audioPlayer;

    public void PlaySound(ScriptableAudioPlayback audioPlayer)
    {
        var distFromCamera = Camera.main.transform.position - transform.position;

        // Only play sound if object is within the camera's view
        if(Mathf.Abs(distFromCamera.y) < Camera.main.orthographicSize 
        && Mathf.Abs(distFromCamera.x) < Camera.main.orthographicSize * Camera.main.aspect)
        {
            audioPlayer.Play();
        }
    }

    public void PlaySound() => PlaySound(audioPlayer);
}
