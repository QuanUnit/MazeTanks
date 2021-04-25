using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float forceOfShot;
    [SerializeField] protected float rangeSpawnOfBullet;
    [SerializeField] protected uint countOfBullets;

    protected PlayerController ownerPlayer;
    private void Start()
    {
        ownerPlayer = GetComponentInParent<PlayerController>();
    }
    public virtual void Shot()
    {
        GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position + transform.up * rangeSpawnOfBullet, Quaternion.identity);
        GameManager.Instance.AddDestroyedObjectAfterRaund(spawnedBullet);
        spawnedBullet.GetComponent<Bullet>().OnDestroy += GameManager.Instance.RemoveDestroyedObjectAfterRaund;
        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * forceOfShot, ForceMode2D.Impulse);
        spawnedBullet.GetComponent<Bullet>().OwnerGun = this;
        countOfBullets--;
    }
    public void IncreaseCountOfBullets()
    {
        countOfBullets++;
    }
}
