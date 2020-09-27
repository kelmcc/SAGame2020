using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyWaveContainerPrefab;

    public EnemyContainer EnemyContainerInstance;

    void Start()
    {
        SpawnEnemies(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnEnemies(int count)
    {
        Transform spawn = DifficultyManager.Instance.GetSpawnTransform();
        EnemyContainerInstance = Instantiate(EnemyWaveContainerPrefab).GetComponent<EnemyContainer>();
        EnemyContainerInstance.transform.position = spawn.transform.position;
    }
}
