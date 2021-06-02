using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [HideInInspector] public event Action<GameObject> OnDestroy;

    [SerializeField] protected float lifeTime;

    Coroutine lifeCycleCorutine;

    protected void Start()
    {
        lifeCycleCorutine = StartCoroutine(LifeCycle(lifeTime));
    }
    protected virtual IEnumerator LifeCycle(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyBullet();
    }
    protected virtual void DestroyBullet()
    {
        if (lifeCycleCorutine != null)
            StopCoroutine(lifeCycleCorutine);
        OnDestroy?.Invoke(gameObject);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Tank tank;
        if (collision.gameObject.TryGetComponent<Tank>(out tank) == true)
        {
            tank.TakeHit();
            DestroyBullet();
        }
    }
}
