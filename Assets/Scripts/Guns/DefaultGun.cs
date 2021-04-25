using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Gun
{
    private void Update()
    {
        if (Input.GetKeyDown(ownerPlayer.Input.GetShotButtonKey()))
        {
            Shot();
        }
    }
    protected override void BulletRegistation(GameObject bullet)
    {
        base.BulletRegistation(bullet);
        bullet.GetComponent<Bullet>().OnDestroy += delegate { IncreaseCountOfBullets(); };
    }
}
