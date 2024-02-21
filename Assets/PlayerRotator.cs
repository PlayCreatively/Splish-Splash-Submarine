using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    public float rotationSpeed = .6f;
    public float reCenteringSpeed = .2f;
    public float maxAngle = 25f;

    void Update()
    {
        // horizontal input
        float input = 0;

        if(Input.GetKey(KeyCode.A))
            input -= 1;
        else if (Input.GetKey(KeyCode.D))
            input += 1;

        float angle = transform.localEulerAngles.z;
        angle = (angle > 180) ? angle - 360 : angle;

        if (input == 0 || input != -Mathf.Sign(angle))
            angle = Mathf.MoveTowards(angle, 0, maxAngle * Time.deltaTime / reCenteringSpeed);

        angle -= input * maxAngle * Time.deltaTime / rotationSpeed;
        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
