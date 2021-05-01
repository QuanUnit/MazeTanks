using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    private void Start()
    {
        StartCoroutine(LifeCycle(lifeTime));
    }
}
