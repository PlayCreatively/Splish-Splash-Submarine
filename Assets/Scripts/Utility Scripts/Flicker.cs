using UnityEngine;
using UnityEngine.Events;

public class Flicker : MonoBehaviour
{
    public float frequency = 1;
    public UnityEvent<bool> flicker;

    Timer frequencyTimer;

    void Start()
    {
        frequencyTimer = new Timer(frequency);
    }

    void Update()
    {
        if(frequencyTimer > 1.2f)
            flicker?.Invoke(true);
    }

    public void UpdateFlicker(float onRatio)
    {

        if (frequencyTimer)
        {
            onRatio = Mathf.Clamp01(onRatio);
            bool on = Random.value < onRatio;
            flicker?.Invoke(on);

            frequencyTimer.Restart();
        }

    }
}
