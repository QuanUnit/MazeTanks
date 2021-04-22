using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager>
{
    public GameMode CurrentGameMode { private get; set; } = GameMode.Duo;
    [Header("Links")]
    [SerializeField] private Counter counterOnStartRaund;
    [SerializeField] private List<GameObject> tanksPrefabs;
    [Header("GameSettings")]
    [SerializeField] [Range(0.5f, 3)] private float preparationTimeForNewRaund = 1.5f;

    private RaundPhase raundPhase;
    private List<Player> players = new List<Player>();
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
            players.Add(player);
            player.TankDeath += delegate { StartCoroutine(PreparingForNewRaund()); };
            AddDestroyedObjectAfterRaund(createdTank);
            reserviredPos = createdTank.transform.position;
        }
    }
    private IEnumerator PreparingForNewRaund()
    {
        if(raundPhase != RaundPhase.PreparingForNewRaund)
        {
            raundPhase = RaundPhase.PreparingForNewRaund;
            yield return new WaitForSeconds(preparationTimeForNewRaund);
            DestroyGameObjectsAfterRaund();
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
        foreach (var player in players)
        {
            player.Input.SwitchState(CustomInput.InputState.Disabled);
        }
    }
    private void FinishCountDown()
    {
        raundPhase = RaundPhase.Playing;
        foreach (var player in players)
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
