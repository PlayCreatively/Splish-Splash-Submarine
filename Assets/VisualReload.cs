using UnityEngine;

public class VisualReload : MonoBehaviour
{
    float curReloadTime = 0;
    float reloadTime;
    public Shooter shooter;
    void Awake()
    {
        reloadTime = GlobalSettings.Current.shooting.reloadTime;
    }
    void Start()
    {
        transform.localScale = new Vector3(0, 1, 1);
        shooter.onEmptyMag.AddListener(() => curReloadTime = 0); 
    }

    void Update()
    {
        if (curReloadTime < 1)
            curReloadTime += Time.deltaTime / reloadTime;

        transform.localScale = new Vector3(curReloadTime, 1, 1);
    }
}
