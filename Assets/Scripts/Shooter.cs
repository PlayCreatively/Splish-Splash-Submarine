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
        // check if the player has bullets left in the magazine
        if (!canShoot)
        {
            if (reloadTime)
            {
                bulletsInMag = magSize;
                canShoot = true;
                onReloaded?.Invoke();
            }
            else
                return;
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            Shoot();
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
        Rigidbody2D bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, .5f), Quaternion.identity);
        bullet.velocity = transform.up * GlobalSettings.Current.shooting.bulletSpeed;

        float secondsTillOutOfBounds = Camera.main.orthographicSize * 2 / GlobalSettings.Current.shooting.bulletSpeed;
        Destroy(bullet.gameObject, secondsTillOutOfBounds);
    }
}
