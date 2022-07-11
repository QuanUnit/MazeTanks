using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : Gun
{
    protected override void BulletRegistration(GameObject bullet)
    {
        base.BulletRegistration(bullet);
        bullet.GetComponent<Bullet>().OnDestroy += actionOfChangeGun;
        ShotTrajectoryRenderer trajectoryRenderer;
        if (TryGetComponent(out trajectoryRenderer))
            trajectoryRenderer.enabled = false;
    }
}
