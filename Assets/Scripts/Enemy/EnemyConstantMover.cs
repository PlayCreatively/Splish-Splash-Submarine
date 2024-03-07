using UnityEngine;

class EnemyConstantMover: MonoBehaviour
{
    void Update()
    {
        transform.position += GameState.Get.playerVerticalSpeed * Time.deltaTime * Vector3.down;
    }
}
