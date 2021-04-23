using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : Singleton<BonusSpawner>
{
    [SerializeField] private List<GameObject> allBonusPrefabs;
    [SerializeField] private float minTimeOfSpawn = 5f;
    [SerializeField] private float maxTimeOfSpawn = 10f;

    private List<GameObject> bonusesInScene = new List<GameObject>();
    public void LaunchSpawnBonuses(int playersCount)
    {
        StartCoroutine(SpawnBonuses(playersCount));
    }
    private IEnumerator SpawnBonuses(int playerCount)
    {
        while(true)
        {
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
        spawnedBonus.GetComponent<Bonus>().OnDestroy += SomeBonusDeath;
        bonusesInScene.Add(spawnedBonus);
    }
    private void SomeBonusDeath(GameObject bonus)
    {
        bonusesInScene.Remove(bonus);
    }
}
