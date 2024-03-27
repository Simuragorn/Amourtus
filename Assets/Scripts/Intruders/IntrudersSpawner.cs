using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntrudersSpawner : MonoBehaviour
{
    [SerializeField] protected List<Intruder> intruderPrefabs;
    [SerializeField] protected int intrudersCount = 5;
    [SerializeField] protected float unitSpawnDelay = 1f;
    [SerializeField] protected Floor spawnFloor;
    [SerializeField] protected Transform spawnPoint;

    private void Awake()
    {
        StartCoroutine(SpawnAll());
    }

    protected IEnumerator SpawnAll()
    {
        GameObject wave = new GameObject("Intruders");
        for (int i = 0; i < intrudersCount; i++)
        {
            SpawnOne(wave);
            yield return new WaitForSeconds(unitSpawnDelay);
        }
    }

    protected void SpawnOne(GameObject wave)
    {
        Intruder prefab = GetIntruderForSpawn();
        Intruder intruder = Instantiate(prefab, spawnPoint.position, Quaternion.identity, wave.transform);
        intruder.SetFloor(spawnFloor);
    }

    protected Intruder GetIntruderForSpawn()
    {
        int intruderIndex = Random.Range(0, intruderPrefabs.Count);
        return intruderPrefabs[intruderIndex];
    }
}
