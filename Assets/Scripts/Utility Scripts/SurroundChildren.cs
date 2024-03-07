using UnityEngine;

public class SurroundChildren : MonoBehaviour
{
    public float radius = 1;

    [Range(-180, 180)]
    public float angle = 0;
    [Range(0, 180)]
    public float coneAngle = 180;

    int childCount;

    void Update()
    {
        if(childCount != transform.childCount)
        {
            childCount = transform.childCount;
            Distribute();
        }
    }

    void OnValidate()
    {
        Update();
        Distribute();
    }

    void Distribute()
    {
        float angleStep = coneAngle * 2f / (childCount);
        int i = 0;
        foreach(Transform child in transform)
        {
            float angleOffset = -coneAngle + angleStep * .5f + angleStep * i;
            float angleInRadians = (angle + angleOffset) * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleInRadians);
            float y = Mathf.Sin(angleInRadians);

            child.localPosition = new Vector2(x, y) * radius;
            i++;
        }
    }
}
