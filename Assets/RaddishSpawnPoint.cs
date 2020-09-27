using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaddishSpawnPoint : MonoBehaviour
{
    public static List<RaddishSpawnPoint> SpawnPoints = new List<RaddishSpawnPoint>();
    private Raddish _radish;
    public Transform root;

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
        bool success = (RaddishPool.TryGetNewRadish(root.position, out Raddish instance));
        if (success)
        {
            _radish = instance;
            _radish.transform.SetParent(root);
            _radish.transform.localRotation = Quaternion.identity;
            _radish.transform.localPosition = Vector3.zero;
        }
        return success;
    }

    private void Update()
    {
        if (HasRadish && !_radish.HasSeekTarget)
        {
            _radish.transform.SetParent(root);
            _radish.transform.localRotation = Quaternion.identity;
            _radish.transform.localPosition = Vector3.zero;
        }
    }

    public bool HasRadish => _radish != null;
}
