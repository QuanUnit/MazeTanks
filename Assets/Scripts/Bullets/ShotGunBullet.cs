using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : Bullet
{
    private void Start()
    {
        lifeTime = Random.Range(lifeTime - 0.1f, lifeTime + 0.1f);
        base.Start();
    }
}
