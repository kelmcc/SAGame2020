using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VisibilityEvent : MonoBehaviour
{
    public event Action OnVisible = delegate {};
    public event Action OnInvisible = delegate {};

    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VisibilityVolume")
        {
            OnVisible();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "VisibilityVolume")
        {
            OnInvisible();
        }
    }
}
