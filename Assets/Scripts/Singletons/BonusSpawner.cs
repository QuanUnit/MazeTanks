using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : Singleton<BonusSpawner>
{
    [SerializeField] private List<GameObject> allBonusPrefabs;
    [SerializeField] private float minTimeOfSpawn = 5f;
    [SerializeField] private float maxTimeOfSpawn = 10f;

    private List<Vector3> positionsOfbonusesInScene = new List<Vector3>();
    private Coroutine coroutine;
    public void LaunchSpawnBonuses(int playersCount)
    {
        positionsOfbonusesInScene.Clear();
        coroutine = StartCoroutine(SpawnBonuses(playersCount));
    }
    public void StopSpawnBonuses()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
    }
    private IEnumerator SpawnBonuses(int playerCount)
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeOfSpawn, maxTimeOfSpawn));
            if(positionsOfbonusesInScene.Count < playerCount * 2)
                SpawnBonus();
        }
    }
    //private void Update()
    //{
    //    Debug.Log(positionsOfbonusesInScene.Count);
    //}
    private void SpawnBonus()
    {
        Vector3 posForSpawn = Vector3.back;
        while (true)
        {
            posForSpawn = MapGenerator.Instance.CellsGrid[Random.Range(0, MapGenerator.Instance.CellsGrid.Count)].pos;
            if (positionsOfbonusesInScene.Contains(posForSpawn) == false)
                break;
        }
        GameObject spawnedBonus = Instantiate(allBonusPrefabs[Random.Range(0, allBonusPrefabs.Count)], posForSpawn, Quaternion.Euler(0, 0, Random.Range(0, 361)));
        Bonus bonus = spawnedBonus.GetComponent<Bonus>();
        bonus.OnDestroy += GameManager.Instance.RemoveDestroyedObjectAfterRaund;
        bonus.OnDestroy += SomeBonusDeath;
        GameManager.Instance.AddDestroyedObjectAfterRaund(spawnedBonus);
        positionsOfbonusesInScene.Add(spawnedBonus.transform.position);
    }
    private void SomeBonusDeath(GameObject bonus)
    {
        positionsOfbonusesInScene.Remove(bonus.transform.position);
    }
}
