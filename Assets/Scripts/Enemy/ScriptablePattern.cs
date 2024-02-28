using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPattern", menuName = "Pattern", order = 1)]
public class ScriptablePattern: ScriptableObject
{
    [Range(0, 1)]
    public float magnitudeRatio = .5f, frequencyRatio = .5f;
    [Range(-.5f, .5f)]
    public float phaseOffset;
    public bool linearX;
    public bool linearY;
}
