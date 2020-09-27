using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyWaveContainerPrefab;

    public static EnemyContainer EnemyContainerInstance;
    public static EnemyManager Instance;

    public float enemyLife = 1;
    public float enemyLifeIncrement = 0.2f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnEnemies(1);
    }

    public void Die(EnemyContainer container)
    {
        Destroy(container.gameObject);
        EnemyContainerInstance = null;
    }

    private void Update()
    {
        if (EnemyContainerInstance == null)
        {
            enemyLife += enemyLifeIncrement;
            SpawnEnemies(1);
        }
    }


    private void SpawnEnemies(int count)
    {
        Transform spawn = DifficultyManager.Instance.GetSpawnTransform();
        EnemyContainerInstance = Instantiate(EnemyWaveContainerPrefab).GetComponent<EnemyContainer>();
        EnemyContainerInstance.Life = enemyLife;
        EnemyContainerInstance.transform.position = spawn.transform.position;
    }
}
