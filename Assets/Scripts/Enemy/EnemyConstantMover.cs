using UnityEngine;

class EnemyConstantMover: MonoBehaviour
{
    void Update()
    {
        transform.position += GameState.Get.PlayerVerticalSpeed * Time.deltaTime * Vector3.down;
    }
}
