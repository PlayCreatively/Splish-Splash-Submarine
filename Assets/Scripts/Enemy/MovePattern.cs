using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MovePattern : MonoBehaviour
{
    public List<Oscillation> oscillations = new(1);

    public float constMovement;
    public float angle;

    [Min(1)]
    public float halfBounds;
    public float boundsOffset;
    [Header("Gizmos\nPreview time")]
    [SerializeField] float previewLength = 3;
    float timeElapsed;
    Transform child;

    private void Start()
    {
        timeElapsed = 0;
        child = transform.GetChild(0);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (Application.isPlaying)
        {
            child.localPosition = oscillations.Evaluate(timeElapsed);
            transform.position += Time.deltaTime * GetConstMovementVector();
        }
        
    }

    Vector3 GetConstMovementVector()
    {
        const float HALF_PI = MathF.PI * .5f;
        return constMovement * new Vector3(MathF.Cos(angle - HALF_PI), MathF.Sin(angle - HALF_PI));
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        int lineCount = (int)(previewLength * oscillations.MaxMagnitude() / oscillations.MinOscillationTime()) * 10;
        Vector3 lastPoint = oscillations.Evaluate(0) + transform.position;
        for (int i = 1; i < lineCount; i++)
        {
            float time = i / (float)(lineCount - 1);
            time *= previewLength;
            Vector3 point = oscillations.Evaluate(time) + transform.position + GetConstMovementVector() * time;

            Gizmos.DrawLine(lastPoint, point);
            lastPoint = point;
        }

        Gizmos.color = Color.green;
        float halfHeight = lastPoint.y - transform.position.y;
        Vector3 cubeOffset = new(boundsOffset, .5f * halfHeight);
        Gizmos.DrawWireCube(transform.position + cubeOffset, new Vector3(halfBounds * 2, halfHeight, 0));


        // Only update in editor if selected
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            timeElapsed %= previewLength;
            child.localPosition = oscillations.Evaluate(timeElapsed) + timeElapsed * GetConstMovementVector();

            UnityEditor.SceneView.RepaintAll();
        }
    }
#endif
}

[Serializable]
public class Oscillation
{
    public ScriptablePattern pattern;
    [Min(.2f)]
    public float magnitude = 1;
    [Min(0.1f), Tooltip("Seconds for a single oscillation")]
    public float oscillationTime = 1;
    [Min(0)]
    public float timingOffset;

    public Vector3 Evaluate(float t)
    {
        if(pattern == null)
            return Vector3.zero;

        t = (t + timingOffset) / oscillationTime;
        return new Vector3(Evaluate(t * pattern.frequencyRatio * 2 + pattern.phaseOffset, pattern.linearX) * magnitude * pattern.magnitudeRatio, Evaluate(t * (1 - pattern.frequencyRatio) * 2, pattern.linearY) * magnitude * (1 - pattern.magnitudeRatio));
    }

    /// <summary>
    /// Evaluate the oscillation function at a given time where t = 1 is one oscillation.
    /// </summary>
    float Evaluate(float t, bool linearInterp)
    {
        return linearInterp ? (Mathf.PingPong(t, 1f) - .5f) * 2 : Mathf.Sin(t * Mathf.PI * 2);
    }
}

public static class OscillationHelper
{
    public static Vector3 Evaluate(this List<Oscillation> oscillations, float t)
    {
        Vector3 result = Vector3.zero;

        foreach (Oscillation osc in oscillations)
        {

            result += osc.Evaluate(t);
            //offset = Vector2.SignedAngle(Vector2.up, result) * Mathf.Deg2Rad / MathF.PI;
        }

        return result;
    }

    public static float MaxMagnitude(this List<Oscillation> oscillations)
    {
        float result = float.MinValue;

        foreach (Oscillation osc in oscillations)
            result = MathF.Max(result, osc.magnitude);

        return result;
    }
    public static float MinOscillationTime(this List<Oscillation> oscillations)
    {
        float result = float.MaxValue;

        foreach (Oscillation osc in oscillations)
            result = MathF.Min(result, osc.oscillationTime);

        return result;
    }

}