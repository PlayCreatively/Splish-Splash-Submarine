using System;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    float distance = 0;
    public int timeToTarget = 40;
    public Text distanceText;
    public Transform preassurePin;

    void Start()
    {
        transform.localScale = new Vector3(1, 0, 1);
    }

    void Update()
    {
        // Increase the distance by 1 every timeToTarget seconds
        distance += Time.deltaTime / timeToTarget;

        transform.localScale = new Vector3(1, distance % 1, 1);

        distanceText.text = (Math.Floor(distance) * 1000 + 1000).ToString() + "m";

        preassurePin.localRotation = Quaternion.Euler(0, 0, -distance * 360);
    }
}
