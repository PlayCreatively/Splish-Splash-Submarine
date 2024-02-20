using UnityEngine;

class EnemyConstantMover: MonoBehaviour
{
    void Update()
    {
        transform.position += GlobalSettings.Current.player.curVerticalSpeed * Time.deltaTime * Vector3.down;
    }
}
