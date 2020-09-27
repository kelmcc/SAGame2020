using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaddishSpawnPoint : MonoBehaviour
{
    public static List<RaddishSpawnPoint> SpawnPoints = new List<RaddishSpawnPoint>();
    private Raddish _radish;

    public void Awake()
    {
        SpawnPoints.Add(this);
    }

    private void OnDestroy()
    {
        SpawnPoints.Remove(this);
    }

    public bool SpawnRaddish()
    {
        bool success = (RaddishPool.TryGetNewRadish(transform.position, out Raddish instance));
        if (success)
        {
            _radish = instance;
            _radish.transform.SetParent(transform);
        }

        return success;
    }

    public bool HasRadish => _radish = null;
}
