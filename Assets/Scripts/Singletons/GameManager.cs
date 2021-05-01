using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager>
{
    public GameMode CurrentGameMode { private get; set; } = GameMode.Duo;
    public List<Tank> Tanks { get; private set; } = new List<Tank>();

    [Header("Links")]
    [SerializeField] private Counter counterOnStartRaund;
    [SerializeField] private List<GameObject> tanksPrefabs;
    [Header("GameSettings")]
    [SerializeField] [Range(0.5f, 3)] private float preparationTimeForNewRaund = 1.5f;

    private RaundPhase raundPhase;
    private List<GameObject> destroyedGameObjectAfterRaund = new List<GameObject>();

    private void Start()
    {
        counterOnStartRaund.OnStartedCount += StartCountDown;
        counterOnStartRaund.OnFinishedCount += FinishCountDown;
        LaunchNewRound();
    }
    public void AddDestroyedObjectAfterRaund(GameObject go)
    {
        destroyedGameObjectAfterRaund.Add(go);
    }
    public void RemoveDestroyedObjectAfterRaund(GameObject go)
    {
        destroyedGameObjectAfterRaund.Remove(go);
    }
    private void Update()
    {
        Debug.Log(destroyedGameObjectAfterRaund.Count);
    }
    private void LaunchNewRound()
    {
        AddDestroyedObjectAfterRaund(MapGenerator.Instance.MapGeneration());
        switch (CurrentGameMode)
        {
            case GameMode.Solo:
                break;
            case GameMode.Duo:
                SpawnPlayers(2);
                break;
            default:
                throw new System.Exception("Mode not found");
        }
        counterOnStartRaund.StartCount(3);
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
            Tank tank = createdTank.GetComponent<Tank>();

            tank.OnDestroy += RemoveDestroyedObjectAfterRaund;
            AddDestroyedObjectAfterRaund(createdTank);

            Tanks.Add(tank);
            tank.OnDestroy += ReactToPlayerDeath;
            reserviredPos = createdTank.transform.position;
        }
    }
    private void ReactToPlayerDeath(GameObject player)
    {
        Tanks.Remove(player.GetComponent<Tank>());
        if(Tanks.Count <= 1)
        {
            StartCoroutine(PreparingForNewRaund());
        }
    }
    private IEnumerator PreparingForNewRaund()
    {
        BonusSpawner.Instance.StopSpawnBonuses();
        if(raundPhase != RaundPhase.PreparingForNewRaund)
        {
            raundPhase = RaundPhase.PreparingForNewRaund;
            yield return new WaitForSeconds(preparationTimeForNewRaund);
            DestroyGameObjectsAfterRaund();
            Tanks.Clear();
            LaunchNewRound();
        }
    }
    private void DestroyGameObjectsAfterRaund()
    {
        for(int i = 0; i < destroyedGameObjectAfterRaund.Count; i++)
        {
            Destroy(destroyedGameObjectAfterRaund[i]);
        }
        destroyedGameObjectAfterRaund.Clear();
    }
    private void StartCountDown()
    {
        raundPhase = RaundPhase.CountDown;
        foreach (var tank in Tanks)
        {
            tank.GetComponent<PlayerController>().Input.SwitchState(CustomInput.InputState.Disabled);
        }
    }
    private void FinishCountDown()
    {
        BonusSpawner.Instance.LaunchSpawnBonuses(Tanks.Count);
        raundPhase = RaundPhase.Playing;
        foreach (var tank in Tanks)
        {
            tank.GetComponent<PlayerController>().Input.SwitchState(CustomInput.InputState.Active);
        }
    }
    public enum GameMode
    {
        Solo,
        Duo,
    }
    public enum RaundPhase
    {
        Playing,
        PreparingForNewRaund,
        CountDown,
    }
}
