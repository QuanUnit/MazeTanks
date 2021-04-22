using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(LifeCycle());
    }
    protected override void DestroyBullet()
    {
        base.DestroyBullet();
        OwnerGun.IncreaseCountOfBullets();
    }
}
