using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaddishPool : MonoBehaviour
{
    public Raddish RaddishPrefab;
    public int TotalRadishes = 50;

    public static bool TryGetNewRadish(Vector3 position, out Raddish raddish)
    {
        if (RaddishesAvailable.Count == 0)
        {
            raddish = null;
            return false;
        }

        Raddish instance = RaddishesAvailable.Dequeue();
        instance.transform.position = position;
        instance.gameObject.SetActive(true);
        raddish = instance;
        return true;
    }

    public static void ReturnRaddish(Raddish raddish)
    {
        raddish.gameObject.SetActive(false);
        RaddishesAvailable.Enqueue(raddish);
    }

    private static Queue<Raddish> RaddishesAvailable = new Queue<Raddish>();

    private void Start()
    {
        for (int i = 0; i < TotalRadishes; i++)
        {
            Raddish instance = Instantiate(RaddishPrefab);
            instance.gameObject.SetActive(false);
            RaddishesAvailable.Enqueue(instance);
        }
    }
}
