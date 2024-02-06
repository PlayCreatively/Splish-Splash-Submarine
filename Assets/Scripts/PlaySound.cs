using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public GameObject soundPlayer;
    private Transform background;

    void Start()
    {
        // OBS: Background has to be named "Background" in the hierarchy
        background = GameObject.Find("Background").transform;
    }

    public void playSound()
    {
        if (transform.position.x > background.position.x - background.localScale.x / 2 &&
            transform.position.x < background.position.x + background.localScale.x / 2 &&
            transform.position.y > background.position.y - background.localScale.y / 2 &&
            transform.position.y < background.position.y + background.localScale.y / 2)
        {
            // Rotation can affect the sound, so we use Quaternion.identity to keep it simple	
            Instantiate(soundPlayer, transform.position, Quaternion.identity);
        }
    }
}
