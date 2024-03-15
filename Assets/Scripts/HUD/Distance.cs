using System;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    const int targetInMeters = 100;
    public Text distanceText;
    public Transform preassurePin;
    public RectTransform distanceBar;

    float maxHeight;

    void Start()
    {
        // is tutorial
        if (GameState.Get.Level == 0) 
        {
            distanceText.text = "0m";
            //preassurePin.localRotation = Quaternion.Euler(0, 0, 0);
            distanceBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            enabled = false;
            return;
        }

        maxHeight = distanceBar.rect.height;

    }

    void Update()
    {
        float progress = GameState.Get.LevelProgress;
        int lastLevel = GameState.Get.Level - 1;

        distanceBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, progress * maxHeight);

        distanceText.text = (Step(progress * targetInMeters + lastLevel * targetInMeters, 5)).ToString() + "m";

        preassurePin.localRotation = Quaternion.Euler(0, 0, -265f * (lastLevel + progress) / 3);
    }

    int Step(float value, int step) => (int)(value / step) * step;
}
