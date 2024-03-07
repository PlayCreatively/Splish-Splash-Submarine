using UnityEngine;

public class VisualReload : MonoBehaviour
{
    public Shooter shooter;
    void Update()
    {
        transform.localScale = new Vector3(shooter.reloadTime, 1, 1);
    }
}
