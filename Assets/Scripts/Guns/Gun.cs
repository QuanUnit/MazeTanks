using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float forceOfShot;
    [SerializeField] protected float rangeSpawnOfBullet;
    [SerializeField] protected uint countOfBullets;
    protected Player ownerPlayer;
    private void Start()
    {
        ownerPlayer = GetComponentInParent<Player>();
    }
    public virtual void Shot()
    {
        countOfBullets--;
    }
    public void IncreaseCountOfBullets()
    {
        countOfBullets++;
    }
}
