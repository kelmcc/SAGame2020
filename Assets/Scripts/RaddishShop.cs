using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaddishCarrier))]
[DefaultExecutionOrder(-100)]
public class RaddishShop : MonoBehaviour
{
    public List<int> LevelRequirements = new List<int>(new[]{ 2, 3, 4});
    public int CurrentLevel = 0;

    public float TimeToAddRaddish = 0.5f;

    private List<Raddish> RaddishesWaitingAtTarget = new List<Raddish>();
    private List<Raddish> RaddishesUsed = new List<Raddish>();

    private InteractorEvent intearactorEvent;
    private Interactor _currentInteractor;
    private float interactionTime;
    private RaddishCarrier _carrier;

    public Transform[] RaddishTargets;

    public event Action<int> OnLevelUp = delegate{};

    public bool MaxLevel { get; private set; }

    private bool _disableShop;
    public bool DisableShop
    {
        get
        {
            return _disableShop;
        }
        set
        {
            _disableShop = value;
            SetRaddishTargetsForNewLevel();
        }
    }
    private void Awake()
    {
        intearactorEvent = GetComponent<InteractorEvent>();
        _carrier = GetComponent<RaddishCarrier>();
    }

    void Start()
    {
        intearactorEvent.OnInteractorEnter += OnInteractorEnter;
        intearactorEvent.OnIntearactorStay += OnInteractorStay;
        intearactorEvent.OnIntearactorLeave += OnInteractorExit;
        SetRaddishTargetsForNewLevel();
    }

    void OnInteractorEnter(Interactor interactor)
    {
        if (MaxLevel || DisableShop) return;
        if ((_currentInteractor != null && interactor.Priority <= _currentInteractor.Priority))
        {
            return;
        }
        _currentInteractor = interactor;
    }

    // Update is called once per frame
    void OnInteractorStay(Interactor interactor)
    {
        if (MaxLevel || DisableShop) return;
        if (_currentInteractor == interactor)
        {
            if (!interactor.TryingToBuild())
            {
                interactionTime = 0;
                CancelRadishes();
                return;
            }

            RaddishCarrier carrier = interactor.GetComponentInParent<RaddishCarrier>();
            Debug.Assert(carrier!= null, "Need raddishcarrier in interactor parents");

            interactionTime += Time.fixedDeltaTime;
            if (interactionTime >= TimeToAddRaddish)
            {
                Raddish raddish = carrier.RemoveForNewTarget(RaddishTargets[RaddishesWaitingAtTarget.Count]);
                if (raddish != null)
                {
                    RaddishesWaitingAtTarget.Add(raddish);
                    interactionTime = 0;
                }

                if (EnoughRadishesToLevel())
                {
                    foreach (Raddish r in RaddishesWaitingAtTarget)
                    {
                        r.AnimateOutAndHide();
                    }
                    RaddishesUsed.AddRange(RaddishesWaitingAtTarget);
                    RaddishesWaitingAtTarget.Clear();

                    if (CurrentLevel < LevelRequirements.Count-1)
                    {
                        CurrentLevel += 1;
                        OnLevelUp?.Invoke(CurrentLevel);
                    }
                    else
                    {
                        MaxLevel = true;
                    }

                    SetRaddishTargetsForNewLevel();
                }
            }
        }
    }

    private void SetRaddishTargetsForNewLevel()
    {
        for(int i = 0; i < RaddishTargets.Length; i++)
        {
            JuiceAnimation anim = RaddishTargets[i].GetComponentInChildren<JuiceAnimation>();
            if (LevelRequirements[CurrentLevel] > i && !MaxLevel && !DisableShop)
            {
                anim.PopScale(anim.transform.localScale.x, 1, 0.5f);
            }
            else
            {
                anim.LerpScale(anim.transform.localScale.x, 0, 0.5f);
            }
        }
    }

    private void CancelRadishes()
    {
        foreach (Raddish raddish in RaddishesWaitingAtTarget)
        {
            raddish.SeekLastTarget();
        }
        RaddishesWaitingAtTarget.Clear();
    }

    private bool EnoughRadishesToLevel()
    {
        return LevelRequirements[CurrentLevel] == RaddishesWaitingAtTarget.Count;
    }

    void OnInteractorExit(Interactor interactor)
    {
        if (MaxLevel || DisableShop) return;
        CancelRadishes();
        if (!interactor.TryingToBuild() || _currentInteractor != interactor)
        {
            return;
        }
    }

}
