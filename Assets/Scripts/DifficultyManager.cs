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

    public Transform[] EnemySpawnPoints;

    public static DifficultyManager Instance;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Time.time - RaddishSpawnInterval > _lastSpawn)
        {
            Debug.Log("Spawning Raddishes");
            SpawnNewRaddishes();
            _lastSpawn = Time.time;
        }
    }

    public float GetFurthestBuildingZ()
    {
        float furthestZ = float.NegativeInfinity;
        foreach (Building building in Buildings)
        {
            if (building.Level > 0)
            {
                if (building.transform.position.z > furthestZ)
                {
                    furthestZ = building.transform.position.z;
                }
            }
        }
        return furthestZ;
    }

    private void SpawnNewRaddishes()
    {
        float furthestZ = GetFurthestBuildingZ();
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
      //  ellegibleSpawns.Sort((rhs, lhs) => rhs.transform.position.z.CompareTo(lhs.transform.position.z));

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


    public Transform GetSpawnTransform()
    {
        float furthestZ = GetFurthestBuildingZ();
        List<Transform> validTransforms = new List<Transform>();
        foreach (var transform in EnemySpawnPoints)
        {
            if (transform.position.z > furthestZ)
            {
                validTransforms.Add(transform);
            }
        }

        Transform smallest = validTransforms[0];
        foreach (var transform in EnemySpawnPoints)
        {
            if (smallest.position.z < transform.position.z)
            {
                smallest = transform;
            }
        }

        return smallest;
    }
}
