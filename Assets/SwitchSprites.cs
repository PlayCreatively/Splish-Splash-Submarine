using System.Linq;
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
    public void ChangeToIf(Sprite sprite, bool condition)
    {
        if (condition)
            GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void ChangeToIfs(Sprite sprite, bool[] conditions)
    {
        // Check if all conditions are true
        if (conditions.All(c => c))
            GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void Default()
    {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
    }
}
