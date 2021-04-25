using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [HideInInspector] public event Action<GameObject> OnDestroy;

    [SerializeField] private GameObject gun;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tank tank;
        if(collision.TryGetComponent<Tank>(out tank) == true)
        {
            Debug.Log("Player gun is changed!");
            //player.ChangeGun(gun.GetComponent<Gun>());
            DestroyBonus();
        }
    }
    private void DestroyBonus()
    {
        OnDestroy?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
