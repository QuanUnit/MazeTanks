using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Gun
{
    private void Update()
    {
        if(countOfBullets > 0)
        {
            if (Input.GetKeyDown(ownerPlayer.Input.GetShotButtonKey()))
            {
                Shot();
            }
        }
    }
    public override void Shot()
    {
        base.Shot();
        GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position + transform.up * rangeSpawnOfBullet, Quaternion.identity);
        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * forceOfShot, ForceMode2D.Impulse);
        spawnedBullet.GetComponent<Bullet>().OwnerGun = this;
    }
}
