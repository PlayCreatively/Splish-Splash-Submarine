using UnityEngine;
using UnityEngine.Events;

public class Flicker : MonoBehaviour
{
    public float frequency = 1;
    [Tooltip("The threshold for the flicker to begin flickering. 0.5 means that the flicker will begin flickering when efb is 50% of its way towards you.")]
    [Range(0,1)]
    public float beginFlickerThreshold = 0.5f;

    public UnityEvent<bool> flicker;
    public UnityEvent flickerOn;
    public UnityEvent flickerOff;


    Timer frequencyTimer;

    void Start()
    {
        frequencyTimer = new Timer(frequency);
    }

    public void UpdateFlicker(float onRatio)
    {
        if(onRatio > beginFlickerThreshold)
            return;
            
        if (frequencyTimer)
        {
            onRatio = Mathf.Clamp01(onRatio);
            bool on = Random.value < onRatio;
            flicker?.Invoke(on);

            frequencyTimer.Restart();
        }

    }
}
