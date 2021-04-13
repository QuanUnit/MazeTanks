using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private GameObject player; //test
    private void Start()
    {
        LaunchNewRound();
    }
    private void LaunchNewRound()
    {
        mapGenerator.MapGeneration();
        SpawnPlayers();
    }
    private void SpawnPlayers()
    {
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
