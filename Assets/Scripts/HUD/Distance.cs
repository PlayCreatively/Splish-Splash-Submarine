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
            distanceBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            enabled = false;
            return;
        }

        maxHeight = distanceBar.rect.height;
        distanceText.text = ((int)(GlobalSettings.Current.level.LevelLength * 10)).ToString() + "m";

    }

    void Update()
    {
        float progress = GameState.Get.LevelProgress;
        int lastLevel = GameState.Get.Level - 1;

        distanceBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, progress * maxHeight);


        preassurePin.localRotation = Quaternion.Euler(0, 0, -265f * (lastLevel + progress) / 3);
    }

    int Step(float value, int step) => (int)(value / step) * step;
}
