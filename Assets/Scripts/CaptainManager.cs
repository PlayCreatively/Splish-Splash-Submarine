using UnityEngine;
using UnityEngine.UI;

public class CaptainManager : MonoBehaviour
{
    //SwitchText switchText;
    public Image captainSpeech;

    //public void Say(string line)
    //{
    //    captainText.text = line;
    //}

    public void Say(Sprite sprite)
    {
        captainSpeech.rectTransform.sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
        captainSpeech.sprite = sprite;
    }
}