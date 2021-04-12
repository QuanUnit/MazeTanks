using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;
    private void Start()
    {
        mapGenerator.MapGeneration();
    }
}
