using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelGun : Gun
{
    protected override void Update()
    {
        base.Update();
    }
    private void FractionsRegistration(List<GameObject> fractions)
    {
        foreach (var fraction in fractions)
        {
            base.BulletRegistration(fraction);
        }
    }
    protected override void BulletRegistration(GameObject bullet)
    {
        base.BulletRegistration(bullet);
        ShrapnelBullet shrapnelBullet = bullet.GetComponent<ShrapnelBullet>();

        shrapnelBullet.OnExplode += FractionsRegistration;
        shrapnelBullet.keyForExplode = ownerPlayer.Input.GetShotButtonKey();

        bullet.GetComponent<Bullet>().OnDestroy += actionOfChangeGun;
    }
}
