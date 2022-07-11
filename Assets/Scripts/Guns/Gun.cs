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
    protected List<GameObject> firedBullets = new List<GameObject>();

    protected virtual void Update()
    {
        if (Input.GetKeyDown(ownerPlayer.Input.GetShotButtonKey()))
        {
            Shot();
        }
    }
    private void Start()
    {
        ownerPlayer = GetComponentInParent<PlayerController>();
        tank = GetComponentInParent<Tank>();
        actionOfChangeGun = delegate { if (countOfBullets <= 0 && tank != null && firedBullets.Count == 0) tank.ChangeGun(null); };
    }
    protected virtual void Shot()
    {
        if(countOfBullets > 0)
        {
            GameObject bullet = SpawnBullet(transform.up);
            BulletRegistration(bullet);
            countOfBullets--;
        }
    }
    protected virtual GameObject SpawnBullet(Vector3 shotDiration)
    {
        GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position + transform.up * rangeSpawnOfBullet, Quaternion.identity);
        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(shotDiration * forceOfShot, ForceMode2D.Impulse);
        return spawnedBullet;
    }
    protected virtual void BulletRegistration(GameObject bullet)
    {
        GameManager.Instance.AddDestroyedObjectAfterRaund(bullet);
        firedBullets.Add(bullet);
        bullet.GetComponent<Bullet>().OnDestroy += GameManager.Instance.RemoveDestroyedObjectAfterRaund;
        bullet.GetComponent<Bullet>().OnDestroy += delegate { firedBullets.Remove(bullet); };
    }
}
