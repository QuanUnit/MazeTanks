using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Bullet
{
    private void Start()
    {
        StartCoroutine(LifeCycle(lifeTime));
    }
}
