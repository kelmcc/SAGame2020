using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Actor : MonoBehaviour
{
    public float WalkSpeed;

    private Brain _brain;

    private float movement;

    private Rigidbody _rigidbody;

    private Interactor _interactor;

    public Animator controller;
    public GameObject CharacterRoot;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _interactor = GetComponentInChildren<Interactor>();

    }

    void Start()
    {
        _brain = GetComponent<Brain>();
        Debug.Assert(_brain != null);
    }

    private void Update()
    {
        if (_brain.GetAction())
        {
            if (_interactor)
            {
                _interactor.InteractActive = true;
            }
        }
        else
        {
            if (_interactor)
            {
                _interactor.InteractActive = false;
            }
        }
    }

    private bool idle = true;

    private void FixedUpdate()
    {
        float movement = _brain.GetMovement();
        if (movement > 0)
        {
            if (idle)
            {
                controller.SetTrigger("Move");
                idle = false;
            }
            CharacterRoot.transform.forward = Vector3.forward;
        }
        else if(movement < 0)
        {
            if (idle)
            {
                controller.SetTrigger("Move");
                idle = false;
            }
            CharacterRoot.transform.forward = -Vector3.forward;
        }
        else
        {
            if (!idle)
            {
                controller.SetTrigger("Idle");
                idle = true;
            }

        }

        _rigidbody.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.fixedDeltaTime * WalkSpeed * movement));
    }
}
