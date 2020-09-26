using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaddishCarrier))]
public class RaddishShop : MonoBehaviour
{
    public List<int> LevelRequirements = new List<int>(new[]{ 2, 3, 4});
    private int CurrentLevel = 0;

    public float TimeToAddRaddish = 0.5f;

    private List<Raddish> RaddishesWaitingAtTarget = new List<Raddish>();
    private List<Raddish> RaddishesUsed = new List<Raddish>();

    private InteractorEvent intearactorEvent;
    private Interactor _currentInteractor;
    private float interactionTime;
    private RaddishCarrier _carrier;

    public Transform[] RaddishTargets;

    public event Action<List<Raddish>> OnLevelUp = delegate{};

    public bool MaxLevel { get; private set; }
    public bool DisableShop { get; set; }
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
                    OnLevelUp(RaddishesWaitingAtTarget);
                    foreach (Raddish r in RaddishesWaitingAtTarget)
                    {
                        r.AnimateOutAndHide();
                    }
                    RaddishesUsed.AddRange(RaddishesWaitingAtTarget);
                    RaddishesWaitingAtTarget.Clear();

                    if (CurrentLevel < LevelRequirements.Count-1)
                    {
                        CurrentLevel += 1;
                        SetRaddishTargetsForNewLevel();
                    }
                    else
                    {
                        MaxLevel = true;
                    }
                }
            }
        }
    }

    private void SetRaddishTargetsForNewLevel()
    {
        for(int i = 0; i < RaddishTargets.Length; i++)
        {
            JuiceAnimation anim = RaddishTargets[i].GetComponentInChildren<JuiceAnimation>();
            if (LevelRequirements[CurrentLevel] > i)
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
