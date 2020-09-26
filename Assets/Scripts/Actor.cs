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

    void Start()
    {
        _brain = GetComponent<Brain>();
        Debug.Assert(_brain != null);
    }

    private void FixedUpdate()
    {
        float movement = _brain.GetMovement();
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.fixedDeltaTime * WalkSpeed * movement);
    }
}
