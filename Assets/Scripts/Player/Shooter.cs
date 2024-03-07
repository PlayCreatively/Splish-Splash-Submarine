using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D bulletPrefab;
    public UnityEvent onReloaded, onEmptyMag, onShot;

    public Timer reloadTime;
    bool canShoot = false;

    private void Awake()
    {
        reloadTime.Start(GlobalSettings.Current.shooting.reloadTime);

        GlobalSettings.Current.shooting.onValidate += 
            (shooting) => reloadTime.Start(shooting.reloadTime);
    }

    private void Update()
    {
        if (!canShoot && reloadTime)
            Reload();
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canShoot)
                Shoot();
            else
                onEmptyMag?.Invoke();
        }
    }

    public void Reload()
    {
        canShoot = true;
        onReloaded?.Invoke();
    }

    void Shoot()
    {
        onShot?.Invoke();
        canShoot = false;
        reloadTime.Restart();
        Rigidbody2D bullet = Instantiate(bulletPrefab, transform.position + transform.up * .5f, Quaternion.identity);
        bullet.velocity = transform.up * GlobalSettings.Current.shooting.bulletSpeed;
        bullet.transform.localRotation = transform.localRotation;

        float secondsTillOutOfBounds = Camera.main.orthographicSize * 2 / GlobalSettings.Current.shooting.bulletSpeed;
        Destroy(bullet.gameObject, secondsTillOutOfBounds);
    }
}
