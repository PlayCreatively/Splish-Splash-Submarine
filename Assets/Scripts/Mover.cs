using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Min(0)]
    public float magnitude, frequency;
    [Range(0, 1)]
    public float magnitudeRatio, frequencyRatio;
    [Range(-.5f, .5f)]
    public float phaseOffset;
    [Header("Linear Interpolation\nOFF: circle, ON: linear")]
    public bool linearX, linearY;

    [Header("Gizmos\nPreview line length")]
    [SerializeField] float lineLength = Mathf.PI * 2;

    void Update()
    {
        float x = Time.time * frequency;
        transform.localPosition = Evaluate(x);
    }

    Vector3 Evaluate(float x)
    {
        return new Vector3(Evaluate(x * frequencyRatio + phaseOffset * Mathf.PI, linearX) * magnitude * magnitudeRatio, Evaluate(x * (1 - frequencyRatio), linearY) * magnitude * (1 - magnitudeRatio));
    }

    public float Evaluate(float x, bool linearInterp)
    {
        return linearInterp ? (Mathf.PingPong(x/Mathf.PI, 1) - .5f) * 2 : Mathf.Sin(x);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        int lineCount = (int)(lineLength * magnitude * 4);
        Vector3 lastPoint = Evaluate(0) + transform.parent.position;
        for (int i = 1; i < lineCount; i++)
        {
            Vector3 point = Evaluate(i / (lineCount - 1f) * lineLength) + transform.parent.position;
            //float colorT = lineCount / (lastPoint - point).sqrMagnitude;
            //print(colorT);
            //Gizmos.color = Color.Lerp(Color.blue, Color.red, colorT);
            Gizmos.DrawLine(lastPoint, point);
            lastPoint = point;

        }
    }
}