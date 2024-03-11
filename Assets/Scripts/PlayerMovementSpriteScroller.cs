using UnityEngine;

public class PlayerMovementSpriteScroller : MonoBehaviour
{

    SpriteRenderer[] renderers;
    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    float GetPixelsTraveled() => GameState.Get.distanceTraveled * 16f;

    void Update()
    {
        foreach (SpriteRenderer sr in renderers)
        {
            sr.material.SetVector("_Offset", GetPixelsTraveled() / sr.sprite.texture.height * Vector2.up);
        }
    }
}