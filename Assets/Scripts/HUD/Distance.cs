using System;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    const int targetInMeters = 100;
    public Text distanceText;
    public Transform distanceIndicator;
    public Transform preassurePin;

    float dItopPos;

    void Start()
    {
        transform.localScale = new Vector3(1, 0, 1);

        dItopPos = distanceIndicator.localPosition.y;
    }

    void Update()
    {
        float progress = GameState.Get.LevelProgress;

        transform.localScale = new Vector3(1, progress, 1);

        distanceText.text = (Math.Floor(progress) * targetInMeters + targetInMeters).ToString() + "m";

        distanceIndicator.localPosition = new Vector3(0, progress * dItopPos, 0);

        // Goes at half the speed of the bar
        preassurePin.localRotation = Quaternion.Euler(0, 0, -315f * (GameState.Get.level - 1 + progress) / 3);
    }
}
