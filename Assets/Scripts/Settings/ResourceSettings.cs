using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSettings")]
public class ResourceSettings : ScriptableObject
{
    public float MaxRaddishes = 50;
    public float RaddishesSpawnedPerDayInEnvironment = 6;
    public float MaxRaddishesSpawnedInEnvironment = 20;

    //public float MaxRaddishesFoundPerDayPerFollower;
    //public float MaxRaddishesFoundPerDayPerFollower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
