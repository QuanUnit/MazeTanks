using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [HideInInspector] public event Action<GameObject> OnDestroy;

    [SerializeField] private GameObject gun;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Tank tank;
        if(collision.TryGetComponent<Tank>(out tank) == true && collision.GetComponentInChildren<Gun>().GetType() == typeof(DefaultGun))
        {
            tank.ChangeGun(gun);
            DestroyBonus();
        }
    }
    private void DestroyBonus()
    {
        OnDestroy?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
