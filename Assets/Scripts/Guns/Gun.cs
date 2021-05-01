using System;
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
    protected Tank tank;
    protected Action<GameObject> actionOfChangeGun;
    private void Start()
    {
        ownerPlayer = GetComponentInParent<PlayerController>();
        tank = GetComponentInParent<Tank>();
        actionOfChangeGun = delegate { if (countOfBullets <= 0 && tank != null) tank.ChangeGun(null); };
    }
    protected virtual void Shot()
    {
        if(countOfBullets > 0)
        {
            GameObject bullet = SpawnBullet(transform.up);
            BulletRegistation(bullet);
            countOfBullets--;
        }
    }
    protected virtual GameObject SpawnBullet(Vector3 shotDiration)
    {
        GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position + transform.up * rangeSpawnOfBullet, Quaternion.identity);
        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(shotDiration * forceOfShot, ForceMode2D.Impulse);
        return spawnedBullet;
    }
    protected virtual void BulletRegistation(GameObject bullet)
    {
        GameManager.Instance.AddDestroyedObjectAfterRaund(bullet);
        bullet.GetComponent<Bullet>().OwnerGun = this;
        bullet.GetComponent<Bullet>().OnDestroy += GameManager.Instance.RemoveDestroyedObjectAfterRaund;
    }
    public void IncreaseCountOfBullets()
    {
        countOfBullets++;
    }
}
