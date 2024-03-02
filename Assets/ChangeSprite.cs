using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite otherSprite;
    Sprite originalSprite;
    void Start()
    {
        originalSprite = GetComponent<SpriteRenderer>().sprite;
    }
    public void Change()
    {
        GetComponent<SpriteRenderer>().sprite = otherSprite;
    }
    public void ChangeBack()
    {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
    }
}
