using Unity.VisualScripting;
using UnityEngine;

public class EventsSpriteSwitcher : MonoBehaviour
{
    bool FishesAttached = false;
    bool GameIsOver = false;
    int fishesAttached = 0;
    SwitchSprites switchSprites;
    void Start()
    {
        switchSprites = GetComponent<SwitchSprites>();
    }

    public void OnFishLatch(Sprite sprite)
    {
        FishesAttached = true;
        fishesAttached++;
        switchSprites.ChangeToIf(sprite, !GameIsOver);
    }
    public void OnFishRelease(Sprite sprite)
    {
        fishesAttached--;
        if (fishesAttached <= 0)
            FishesAttached = false;
        switchSprites.ChangeToIf(sprite, !FishesAttached && !GameIsOver);
    }
    public void OnGameOver(Sprite sprite)
    {
        GameIsOver = true;
        switchSprites.ChangeTo(sprite);
    }
}
