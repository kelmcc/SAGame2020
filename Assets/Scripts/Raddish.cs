using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(InteractorEvent), typeof(Rigidbody))]
public class Raddish : MonoBehaviour
{
    public float TimeBeforeSeekingTarget;

    private Transform _lastSeekTarget;
    private Transform _seekTarget;
    private Vector3 _startSeekPosition;
    private float _seekTime;

    private InteractorEvent intearactorEvent;

    private Interactor _currentInteractor;
    private float interactionTime;

    private Rigidbody _rigidbody;

    private JuiceAnimation _juiceAnimation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        intearactorEvent = GetComponent<InteractorEvent>();
        _rigidbody.isKinematic = true;
        _juiceAnimation = GetComponentInChildren<JuiceAnimation>();
    }

    void Start()
    {
        intearactorEvent.OnInteractorEnter += OnInteractorEnter;
        intearactorEvent.OnIntearactorStay += OnInteractorStay;
        intearactorEvent.OnIntearactorLeave += OnInteractorExit;
    }

    void OnInteractorEnter(Interactor interactor)
    {
        if ((_currentInteractor != null && interactor.Priority <= _currentInteractor.Priority))
        {
            return;
        }

        _currentInteractor = interactor;
    }

    public void SetNewTarget(Transform transform)
    {
        UnHide();
        _juiceAnimation.LerpScale(0.25f, 1, 1);
        SeekTarget(transform);
    }

    // Update is called once per frame
    void OnInteractorStay(Interactor interactor)
    {
        if (_currentInteractor == interactor)
        {
            if (!interactor.TryingToPickUpRadish() || _seekTarget != null)
            {
                interactionTime = 0;
                return;
            }

            if (!_juiceAnimation.IsWobbling)
            {
                _juiceAnimation.Wobble(0.04f, 50, 1);
            }

            interactionTime += Time.fixedDeltaTime;
            if (interactionTime >= TimeBeforeSeekingTarget)
            {
                _juiceAnimation.Stop();
                _juiceAnimation.LerpScale(1, 0.25f, 1);
                SeekTarget(_currentInteractor.transform);
            }
        }
    }

    public void SeekTarget(Transform transform)
    {
        _rigidbody.isKinematic = false;
        if(_seekTarget != null)_lastSeekTarget = _seekTarget;
        _seekTarget = transform;
        _seekTime = 0;
        _startSeekPosition = transform.position;
        _rigidbody.AddForce(Vector3.up * 6, ForceMode.Impulse);
    }

    public void UnseekTarget()
    {
        //dont make kinematic?
        if(_seekTarget != null)_lastSeekTarget = _seekTarget;
        _seekTarget = null;
    }

    public void SeekLastTarget()
    {
        _seekTime = 0;
        _seekTarget = _lastSeekTarget;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        UnseekTarget();
    }

    public void AnimateOutAndHide()
    {
        _juiceAnimation.LerpScale(1, 0.25f, 1f, () =>
        {
            Hide();
        });
    }

    public void UnHide()
    {
        gameObject.SetActive(true);
    }

    void OnInteractorExit(Interactor interactor)
    {
        if (!interactor.TryingToPickUpRadish() || _currentInteractor != interactor)
        {
            return;
        }

        _currentInteractor = null;
        interactionTime = float.PositiveInfinity;
    }

    void FixedUpdate()
    {
        if (_seekTarget == null)
        {
            return;
        }

       // _rigidbody.positions

       _seekTime += Time.deltaTime;
       Vector3 lerpPos = Vector3.Lerp(_rigidbody.transform.position, _seekTarget.transform.position, _seekTime);
       _rigidbody.MovePosition(lerpPos);
       _rigidbody.velocity = Vector3.zero;

        if (Vector3.Distance(_seekTarget.position, _rigidbody.position) < 2)
        {
            _seekTarget.GetComponentInParent<RaddishCarrier>().StoreRaddish(this);
        }

    }
}
