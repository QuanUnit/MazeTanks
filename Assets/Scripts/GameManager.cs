using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager>
{
    public GameMode CurrentGameMode { private get; set; } = GameMode.Duo;

    [SerializeField] private List<GameObject> tanksPrefabs;  
    private void Start()
    {
        LaunchNewRound();
    }
    private void LaunchNewRound()
    {
        MapGenerator.Instance.MapGeneration();
        switch (CurrentGameMode)
        {
            case GameMode.Solo:
                break;
            case GameMode.Duo:
                SpawnPlayers(2);
                break;
            default:
                throw new System.Exception("Mode not found");
                break;
        }
    }
    private void SpawnPlayers(int countOfPlayers)
    {
        Vector3 reserviredPos = Vector3.back;
        for(int i = 0; i < countOfPlayers; i++)
        {
            Vector3 posForSpawn;
            do
            {
                posForSpawn = MapGenerator.Instance.CellsGrid[Random.Range(0, MapGenerator.Instance.CellsGrid.Count)].pos;
            } while (reserviredPos == posForSpawn);
            GameObject createdTank = Instantiate(tanksPrefabs[i], posForSpawn, Quaternion.Euler(0, 0, Random.Range(0, 361)));
            reserviredPos = createdTank.transform.position;
        }
    }
    public enum GameMode
    {
        Solo,
        Duo,
    }
}
