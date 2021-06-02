using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Gun
{
    protected override void Update()
    {
        base.Update();
    }
    private void IncreaseCountOfBullets()
    {
        countOfBullets++;
    }
    protected override void BulletRegistation(GameObject bullet)
    {
        base.BulletRegistation(bullet);
        bullet.GetComponent<Bullet>().OnDestroy += delegate { IncreaseCountOfBullets(); };
    }
}
