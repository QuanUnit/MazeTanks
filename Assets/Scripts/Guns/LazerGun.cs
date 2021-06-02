using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : Gun
{
    protected override void Update()
    {
        base.Update();
    }
    protected override void BulletRegistation(GameObject bullet)
    {
        base.BulletRegistation(bullet);
        bullet.GetComponent<Bullet>().OnDestroy += actionOfChangeGun;
        ShotTrajectoryRenderer trajectoryRenderer;
        if (TryGetComponent(out trajectoryRenderer))
            trajectoryRenderer.Disable();
    }
}
