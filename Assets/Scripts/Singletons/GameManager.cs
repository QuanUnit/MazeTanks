using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager>
{
    public GameMode CurrentGameMode { private get; set; } = GameMode.Duo;
    public List<Player> Players { get; private set; } = new List<Player>();

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
                break;
        }
        counterOnStartRaund.StartCount(3);
        BonusSpawner.Instance.LaunchSpawnBonuses(Players.Count);
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
            Player player = createdTank.GetComponent<Player>();
            Players.Add(player);
            player.OnDestroy += ReactToPlayerDeath;
            reserviredPos = createdTank.transform.position;
        }
    }
    private void ReactToPlayerDeath(GameObject player)
    {
        Players.Remove(player.GetComponent<Player>());
        if(Players.Count <= 1)
        {
            StartCoroutine(PreparingForNewRaund());
        }
    }
    private IEnumerator PreparingForNewRaund()
    {
        if(raundPhase != RaundPhase.PreparingForNewRaund)
        {
            raundPhase = RaundPhase.PreparingForNewRaund;
            yield return new WaitForSeconds(preparationTimeForNewRaund);
            DestroyGameObjectsAfterRaund();
            Players.Clear();
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
        foreach (var player in Players)
        {
            player.Input.SwitchState(CustomInput.InputState.Disabled);
        }
    }
    private void FinishCountDown()
    {
        raundPhase = RaundPhase.Playing;
        foreach (var player in Players)
        {
            player.Input.SwitchState(CustomInput.InputState.Active);
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
