using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenManager : MonoBehaviour
{

    public GameObject _root;

    void Start()
    {
        _root.SetActive(true);
    }

    void Update()
    {
        //lol hacks
        if (Input.anyKey)
        {
            _root.SetActive(false);
        }
    }
}
