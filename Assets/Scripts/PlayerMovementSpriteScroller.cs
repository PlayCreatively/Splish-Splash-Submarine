using UnityEngine;

public class PlayerMovementSpriteScroller : MonoBehaviour
{

    SpriteRenderer[] renderers;
    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    float GetScrollSpeed()
    {
        return GameState.Get.PlayerVerticalSpeed * 16f;
    }

    void Update()
    {
        foreach (SpriteRenderer sr in renderers)
        {
            sr.material.SetVector("_Offset", Time.time * GetScrollSpeed() / sr.sprite.texture.height * Vector2.up);
        }
    }
}