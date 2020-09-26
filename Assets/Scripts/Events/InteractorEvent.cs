using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorEvent : MonoBehaviour
{
    public event Action<Interactor> OnInteractorEnter = delegate {};
    public event Action<Interactor> OnIntearactorStay = delegate {};
    public event Action<Interactor> OnIntearactorLeave = delegate {};
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InteractionVolume")
        {
            Interactor interactor = other.GetComponent<Interactor>();
            Debug.Assert(interactor != null);
            OnInteractorEnter(interactor);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "InteractionVolume")
        {
            Interactor interactor = other.GetComponent<Interactor>();
            Debug.Assert(interactor != null);
            OnIntearactorStay(interactor);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "InteractionVolume")
        {
            Interactor interactor = other.GetComponent<Interactor>();
            Debug.Assert(interactor != null);
            OnIntearactorLeave(interactor);
        }
    }
}
