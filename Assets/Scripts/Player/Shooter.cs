using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D bulletPrefab;
    public UnityEvent onReloaded, onEmptyMag, onShot;

    Timer reloadTime;
    bool canShoot = false;
    int magSize;
    int bulletsInMag;

    private void Awake()
    {
        reloadTime.Start(GlobalSettings.Current.shooting.reloadTime);

        GlobalSettings.Current.shooting.onValidate += 
            (shooting) => reloadTime.Start(shooting.reloadTime);

        magSize = GlobalSettings.Current.shooting.magSize;

        onReloaded.AddListener(() => GetComponentInChildren<SpriteRenderer>().color = new Color(1, .6f, .6f));
        onEmptyMag.AddListener(() => GetComponentInChildren<SpriteRenderer>().color = Color.white);
    }

    private void Update()
    {
        if (!canShoot)
            if (reloadTime)
                Reload();
            else
                return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            Shoot();
    }

    public void Reload()
    {
        bulletsInMag = magSize;
        canShoot = true;
        onReloaded?.Invoke();
    }

    void Shoot()
    {
        onShot?.Invoke();
        bulletsInMag--;
        if (bulletsInMag == 0)
        {
            canShoot = false;
            onEmptyMag?.Invoke();
            reloadTime.Restart();
        }
        Rigidbody2D bullet = Instantiate(bulletPrefab, transform.position + transform.up * .5f, Quaternion.identity);
        bullet.velocity = transform.up * GlobalSettings.Current.shooting.bulletSpeed;

        float secondsTillOutOfBounds = Camera.main.orthographicSize * 2 / GlobalSettings.Current.shooting.bulletSpeed;
        Destroy(bullet.gameObject, secondsTillOutOfBounds);
    }
}
