using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D bulletPrefab;

    Timer shootRate;

    private void Awake()
    {
        shootRate.Start(GlobalSettings.Current.shooting.shootRate);

        GlobalSettings.Current.shooting.onValidate += 
            (shooting) => shootRate.Start(shooting.shootRate);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            TryShoot();
    }

    public void TryShoot()
    {
        if (shootRate)
            Shoot();
    }

    void Shoot()
    {
        shootRate.Restart();
        Rigidbody2D bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.velocity = transform.up * GlobalSettings.Current.shooting.bulletSpeed;
    }
}
