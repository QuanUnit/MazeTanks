using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunBullet : Bullet
{
    private void Start()
    {
        StartCoroutine(LifeCycle(lifeTime));
    }
}
