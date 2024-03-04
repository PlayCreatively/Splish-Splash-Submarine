using UnityEngine;

public class LocalTrailRenderer : MonoBehaviour
{
    public LineRenderer trail;
    public SpriteRenderer blip;
    [Min(0)]
    public float maxDistance = 6f;
    [Min(0)]
    public float stepDistance = .1f;
    public bool emitting = true;

    Vector2 lastPoint;
    Transform child;
    int maxPointCount;

    public Color Color
    {
        get => trail.startColor;
        set
        {
            trail.startColor = blip.color = value;
        }
    }

    void Start()
    {
        child = transform.GetChild(0);
        trail = Instantiate(trail, transform, false);
        blip = Instantiate(blip, transform, false);
        trail.useWorldSpace = false;
        maxPointCount = (int)(maxDistance / stepDistance);
    }

    public void Clear()
    {
        trail.positionCount = 0;
    }

    void Update()
    {
        Vector2 curPoint = child.localPosition;

        if (emitting && Vector2.Distance(curPoint, lastPoint) > stepDistance)
            AddPoint(curPoint);
    }

    public void AddPoint(Vector2 point)
    {
        if (trail.positionCount < maxPointCount)
            trail.positionCount++;

        // shift all points to the right
        for (int i = trail.positionCount - 1; i > 0; i--)
            trail.SetPosition(i, trail.GetPosition(i - 1));

        trail.SetPosition(0, point);
        blip.transform.localPosition = point;
        lastPoint = point;
    }
}