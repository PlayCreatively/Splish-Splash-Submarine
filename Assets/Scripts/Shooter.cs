using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D bulletPrefab;
    public UnityEvent onReloaded, onShot;

    Timer shootRate;
    bool canShoot = false;

    private void Awake()
    {
        shootRate.Start(GlobalSettings.Current.shooting.shootRate);

        GlobalSettings.Current.shooting.onValidate += 
            (shooting) => shootRate.Start(shooting.shootRate);

        onReloaded.AddListener(() => GetComponent<SpriteRenderer>().color = new Color(1, .6f, .6f));
        onShot.AddListener(() => GetComponent<SpriteRenderer>().color = Color.white);
    }

    private void Update()
    {
        // check if the player can shoot
        if (!canShoot)
        {
            if (shootRate)
            {
                canShoot = true;
                onReloaded.Invoke();
            }
            else
                return;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            Shoot();
    }

    void Shoot()
    {
        canShoot = false;
        shootRate.Restart();
        onShot.Invoke();
        Rigidbody2D bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.velocity = transform.up * GlobalSettings.Current.shooting.bulletSpeed;

        float secondsTillOutOfBounds = Camera.main.orthographicSize * 2 / GlobalSettings.Current.shooting.bulletSpeed;
        Destroy(bullet.gameObject, secondsTillOutOfBounds);
    }
}
