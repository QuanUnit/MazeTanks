using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [HideInInspector] public event Action<GameObject> OnDestroy;

    [SerializeField] private GameObject gun;

    private void Start()
    {
        OnDestroy += GameManager.Instance.RemoveDestroyedObjectAfterRaund;
        GameManager.Instance.AddDestroyedObjectAfterRaund(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if(collision.TryGetComponent<Player>(out player) == true)
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
