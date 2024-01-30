
using UnityEngine;

class ConstantMover: MonoBehaviour
{
    public Vector2 direction;

    void Update()
    {
        transform.position += (Vector3)(Time.deltaTime * direction);
    }
}
