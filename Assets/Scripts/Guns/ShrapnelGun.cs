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
            base.BulletRegistation(fraction);
        }
    }
    protected override void BulletRegistation(GameObject bullet)
    {
        base.BulletRegistation(bullet);
        ShrapnelBullet shrapnelBullet = bullet.GetComponent<ShrapnelBullet>();

        shrapnelBullet.OnExplode += FractionsRegistration;
        shrapnelBullet.keyForExplode = ownerPlayer.Input.GetShotButtonKey();

        bullet.GetComponent<Bullet>().OnDestroy += actionOfChangeGun;
    }
}
