using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    private void Start()
    {
        StartCoroutine(LifeCycle());
    }
    protected override void DestroyBullet()
    {
        base.DestroyBullet();
        OwnerGun.IncreaseCountOfBullets();
    }
}
