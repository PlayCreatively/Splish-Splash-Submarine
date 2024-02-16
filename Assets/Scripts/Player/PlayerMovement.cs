using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveArea;

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position - Vector3.right * moveArea, transform.position + Vector3.right * moveArea);
    }
    
    void Update()
    {
        float input = 0;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            input--;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            input++;
        if (input == 0)
            return;

        var temp = transform.position;
        temp.x = transform.position.x + input * Time.deltaTime * 5;
        temp.x = Mathf.Clamp(temp.x, -moveArea, moveArea);
        transform.position = temp;
    }
}
