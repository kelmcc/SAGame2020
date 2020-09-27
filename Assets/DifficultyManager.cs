using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DifficultyManager : MonoBehaviour
{
    public List<Building> Buildings;

    private float _lastSpawn;
    public float RaddishSpawnInterval = 60;
    public int NumberOfRadishesSpawned = 4;

    private void Update()
    {
        if (Time.time - RaddishSpawnInterval > _lastSpawn)
        {
            Debug.Log("Spawning Raddishes");
            SpawnNewRaddishes();
            _lastSpawn = Time.time;
        }
    }

    private void SpawnNewRaddishes()
    {
        float furthestZ = float.NegativeInfinity;
        Building furthestBuilding = null;
        foreach (Building building in Buildings)
        {
            if (building.Level > 0)
            {
                if (building.transform.position.z > furthestZ)
                {
                    furthestZ = building.transform.position.z;
                    furthestBuilding = building;
                }
            }
        }

        List<RaddishSpawnPoint> ellegibleSpawns = new List<RaddishSpawnPoint>();
        foreach (var spawnPoint in RaddishSpawnPoint.SpawnPoints)
        {
            if (spawnPoint != null && spawnPoint.transform.position.z > furthestZ && !spawnPoint.HasRadish)
            {
                ellegibleSpawns.Add(spawnPoint);
            }
        }

        //Sort so we spawn from closest to furthest!
        //TODO: double check this is not reversed
        ellegibleSpawns.Sort((rhs, lhs) => rhs.transform.position.z.CompareTo(lhs.transform.position.z));

        for (int i = NumberOfRadishesSpawned; i >= 0; i--)
        {
            if (ellegibleSpawns.Count == 0)
            {
                Debug.LogWarning("Not Enough Spawns. Player is not collecting. ");
                break;
            }
            int spawnIndex = UnityEngine.Random.Range(0, ellegibleSpawns.Count);
            bool spawned = ellegibleSpawns[spawnIndex].SpawnRaddish();
            ellegibleSpawns.RemoveAt(spawnIndex);
        }

    }
}
