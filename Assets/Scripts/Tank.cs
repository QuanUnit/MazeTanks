using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [HideInInspector] public event Action<GameObject> OnDestroy;
    public void TakeHit()
    {
        DestroyTank();
    }
    private void DestroyTank()
    {
        OnDestroy?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
