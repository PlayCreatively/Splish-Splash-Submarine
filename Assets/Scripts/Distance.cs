using System;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    // distance in "targets" reached
    float distance = 0;
    bool isMoving = true;
    const int targetInMeters = 100;
    public int timeToTarget = 30;
    public Text distanceText;
    public Transform preassurePin;
    public float prsPinRelSpeed = 0.4f;
    public EFBLurker efbLurker;

    void Start()
    {
        transform.localScale = new Vector3(1, 0, 1);

        efbLurker.OnCaught.AddListener(() => isMoving = false);
    }

    void Update()
    {
        if (isMoving)
            // Increase the distance by 1 every timeToTarget seconds
            distance += Time.deltaTime / timeToTarget;

        transform.localScale = new Vector3(1, distance % 1, 1);

        if (distance * targetInMeters < 10000)
            distanceText.text = (Math.Floor(distance) * targetInMeters + targetInMeters).ToString() + "m";

        // Goes at half the speed of the bar
        preassurePin.localRotation = Quaternion.Euler(0, 0, -distance * 360 * prsPinRelSpeed);
    }
}
