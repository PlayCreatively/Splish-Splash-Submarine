using UnityEngine;

public class SwitchSprites : MonoBehaviour
{
    public Sprite[] sprites;
    Sprite originalSprite;
    void Start()
    {
        originalSprite = GetComponent<SpriteRenderer>().sprite;
    }
    public void ChangeByIndex(int index)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
    public void ChangeTo(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void Default()
    {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
    }
}
