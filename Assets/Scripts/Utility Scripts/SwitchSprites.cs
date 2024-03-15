using UnityEngine;
using UnityEngine.UI;

public class SwitchSprites : MonoBehaviour
{
    Sprite originalSprite;

    void Start()
    {
        originalSprite = GetComponent<Image>().sprite;
    }
    public void ChangeTo(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
    public void ChangeToIf(Sprite sprite, bool condition)
    {
        if (condition)
            ChangeTo(sprite);
    }
    public void Default()
    {
        GetComponent<Image>().sprite = originalSprite;
    }

    // public Sprite[] Sprites;
    // public void ChangeByIndex(int index)
    // {
    //     GetComponent<SpriteRenderer>().sprite = Sprites[index];
    // }   
    // int GetIndex()
    // {
    //     return Sprites.ToList().IndexOf(GetComponent<SpriteRenderer>().sprite);
    // }
    // int GetNextIndex()
    // {
    //     return (GetIndex() + 1) % Sprites.Length;
    // }
    // int GetPreviousIndex()
    // {
    //     return (GetIndex() - 1) % Sprites.Length;
    // }
}
