using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public enum TankColor
    {
        green,
        red,
    }
    [SerializeField] public TankColor tankColor;

    [HideInInspector] public event Action<GameObject> OnDestroy;
    [SerializeField] private GameObject defaultGun;
    public void TakeHit()
    {
        DestroyTank();
    }
    public void ChangeGun(GameObject gun)
    {
        Destroy(transform.GetComponentInChildren<Gun>().gameObject);
        GameObject spawnedGun;
        if (gun == null)
            spawnedGun = Instantiate(defaultGun, transform);
        else
            spawnedGun = Instantiate(gun, transform);
        spawnedGun.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
    }
    private void DestroyTank()
    {
        OnDestroy?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
