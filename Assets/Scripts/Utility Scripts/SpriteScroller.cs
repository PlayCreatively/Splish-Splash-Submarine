using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    public Vector2 scrollSpeed = Vector2.up;

    SpriteRenderer[] renderers;
    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        foreach (SpriteRenderer sr in renderers)
        {
            sr.material.SetVector("_Offset", Time.time * scrollSpeed / sr.sprite.texture.height);
        }
    }
}
