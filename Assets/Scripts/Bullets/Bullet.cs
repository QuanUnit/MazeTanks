using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [HideInInspector] public event Action<GameObject> OnDestroy;
    public Gun OwnerGun { get; set; }

    [SerializeField] protected float lifeTime;
    protected virtual void Start()
    {
        GameManager.Instance.AddDestroyedObjectAfterRaund(gameObject);
        OnDestroy += GameManager.Instance.RemoveDestroyedObjectAfterRaund;
    }
    protected virtual IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyBullet();
    }
    protected virtual void DestroyBullet()
    {
        OnDestroy?.Invoke(gameObject);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player;
            collision.gameObject.TryGetComponent<Player>(out player);
            player.TakeHit();
            DestroyBullet();
        }
    }
}
