using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Gun OwnerGun { get; set; }

    [SerializeField] protected float lifeTime;
    protected virtual IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyBullet();
    }
    protected virtual void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
