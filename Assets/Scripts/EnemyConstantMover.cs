using UnityEngine;

class EnemyConstantMover: MonoBehaviour
{
    public Vector2 direction = new Vector2(0, -1);
    public float speed;

    void Start()
    {
        speed = GlobalSettings.Current.enemy.fallSpeed;
    }

    void Update()
    {
        speed = GlobalSettings.Current.enemy.curFallspeed;
        transform.position += (Vector3)(speed * Time.deltaTime * direction);
    }
}
