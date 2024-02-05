using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour
{
    [SerializeField, Min(.05f)]
    float translationTime;
    [SerializeField, Min(.05f)]
    float centerTime;
    [SerializeField]
    AnimationCurve moveCurve;

    float x;

    void Update()
    {

        float input = 0;
        if (Input.GetKey(KeyCode.W))
            input += 1;
        if (Input.GetKey(KeyCode.S))
            input -= 1;

        if (input == 0)
            x = Mathf.MoveTowards(x, 0, (Time.deltaTime / centerTime));

        x += input * (Time.deltaTime / translationTime);
        x = Mathf.Clamp(x, -1, 1);

        var temp = transform.localPosition;
        
        float sign = Mathf.Sign(x);
        temp.y = moveCurve.Evaluate(Mathf.Abs(x)) * sign;

        transform.localPosition = temp;
    }
}
