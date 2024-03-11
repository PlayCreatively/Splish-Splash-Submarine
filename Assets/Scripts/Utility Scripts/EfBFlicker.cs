using UnityEngine;
using UnityEngine.Events;

public class EfBFlicker : MonoBehaviour
{
    public float frequency = .1f;
    [Tooltip("The threshold for the flicker to begin flickering. 0.5 means that the flicker will begin flickering when efb is 50% of its way towards you.")]
    [Range(0, 1)]
    public float beginFlickerThreshold = 0.5f;

    [SerializeField]
    SpriteRenderer  efbRend,
                    visibleAreaRend;

    Timer frequencyTimer;
    bool on;

    void Start()
    {
        frequencyTimer = new Timer(frequency);
    }

    public void UpdateFlicker(float onRatio)
    {

        onRatio = 1f - onRatio;
        if (on && onRatio < beginFlickerThreshold)
            return;

        if (frequencyTimer)
        {
            bool on = Random.value > onRatio;

            if(on != this.on)
                Switch(on, onRatio);

            frequencyTimer.Restart();
        }

        if (onRatio > .97f)
            visibleAreaRend.color = Color.black;

    }

    bool GetRandom(float v) => Random.value < v;

    public void Switch(bool on, float t)
    {
        this.on = on;
        efbRend.color = on ? Color.white : t > .9f || GetRandom(.7f) ? Color.black : Color.clear;

        var temp = visibleAreaRend.color;
        temp.a = on ? 0f : .9f;
        visibleAreaRend.color = temp;
    }
}
