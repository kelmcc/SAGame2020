using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(VisibilityEvent))]
public class EnvironmentObject : MonoBehaviour
{
    private Transform _root;

    private float XOffset = 5;
    private float _rotateSpeed = 400;

    private bool _initialValuesSet = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;


    private VisibilityEvent _visibilityEvent;

    public void Awake()
    {
        _root = transform.GetChild(0);
        _root.gameObject.SetActive(false);

        _visibilityEvent = GetComponent<VisibilityEvent>();

        _visibilityEvent.OnVisible += () =>
        {
            StartCoroutine(TurnOn());
        };

        _visibilityEvent.OnInvisible += () =>
        {
            StartCoroutine(TurnOff());
        };
    }

    public IEnumerator TurnOn()
    {
        if (!_initialValuesSet)
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            _initialValuesSet = true;
        }

        yield return new WaitForSeconds(Random.Range(0f, 0.75f));

        _root.gameObject.SetActive(true);
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        float offset = (transform.position.x > transform.parent.position.x)? XOffset: -XOffset;
        Vector3 pivot = new Vector3(transform.parent.position.x + offset, transform.position.y, transform.position.z);
        transform.RotateAround(pivot, Vector3.forward, -180);
        float angle = 180;
        while (angle > 0)
        {
            yield return null;
            float toRotate = Mathf.Min(angle, Time.deltaTime * _rotateSpeed);
            transform.RotateAround(pivot, Vector3.forward, toRotate * Mathf.Sign(offset));
            angle -= toRotate;
        }
    }

    public IEnumerator TurnOff()
    {
        if (!_initialValuesSet)
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            _initialValuesSet = true;
        }

        yield return new WaitForSeconds(Random.Range(0f, 0.25f));
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        float offset = (transform.position.x > transform.parent.position.x)? XOffset: -XOffset;
        Vector3 pivot = new Vector3(transform.parent.position.x + offset, transform.position.y, transform.position.z);
        float angle = 180;
        while (angle > 0)
        {
            yield return null;
            float toRotate = Mathf.Min(angle, Time.deltaTime * _rotateSpeed);
            transform.RotateAround(pivot, Vector3.forward, -toRotate * Mathf.Sign(offset));
            angle -= toRotate;
        }
        _root.gameObject.SetActive(false);
    }
}
