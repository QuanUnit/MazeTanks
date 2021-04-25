using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : Singleton<BonusSpawner>
{
    [SerializeField] private List<GameObject> allBonusPrefabs;
    [SerializeField] private float minTimeOfSpawn = 5f;
    [SerializeField] private float maxTimeOfSpawn = 10f;

    private List<GameObject> bonusesInScene = new List<GameObject>();
    private Coroutine coroutine;
    public void LaunchSpawnBonuses(int playersCount)
    {
        Debug.Log("is started!");
        bonusesInScene.Clear();
        coroutine = StartCoroutine(SpawnBonuses(playersCount));
    }
    public void StopSpawnBonuses()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
        Debug.Log("is stoped!");
    }
    private IEnumerator SpawnBonuses(int playerCount)
    {
        while(true)
        {
            Debug.Log("is working!");
            yield return new WaitForSeconds(Random.Range(minTimeOfSpawn, maxTimeOfSpawn));
            if(bonusesInScene.Count < playerCount * 2)
                SpawnBonus();
        }
    }
    //private void Update()
    //{
    //    Debug.Log(bonusesInScene.Count);
    //}
    private void SpawnBonus()
    {
        Vector3 posForSpawn = MapGenerator.Instance.CellsGrid[Random.Range(0, MapGenerator.Instance.CellsGrid.Count)].pos;
        GameObject spawnedBonus = Instantiate(allBonusPrefabs[Random.Range(0, allBonusPrefabs.Count)], posForSpawn, Quaternion.Euler(0, 0, Random.Range(0, 361)));
        Bonus bonus = spawnedBonus.GetComponent<Bonus>();
        bonus.OnDestroy += GameManager.Instance.RemoveDestroyedObjectAfterRaund;
        bonus.OnDestroy += SomeBonusDeath;
        GameManager.Instance.AddDestroyedObjectAfterRaund(spawnedBonus);
        bonusesInScene.Add(spawnedBonus);
    }
    private void SomeBonusDeath(GameObject bonus)
    {
        bonusesInScene.Remove(bonus);
    }
}
