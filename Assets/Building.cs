using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(InteractorEvent), typeof(RaddishShop))]
public class Building : MonoBehaviour
{
    public float TimeBeforeAddingRaddish;

    private InteractorEvent intearactorEvent;
    private Interactor _currentInteractor;
    private float interactionTime;

    public float BuildTime;
    public float TimeRemaining;
    public int CurrentUpgradeLevel;
    public int TargetUpgradeLevel;

    public GameObject UnderConstructionObject;
    public GameObject[] UpgradeOptions;

    private RaddishShop _raddishShop;

    private void Awake()
    {
	    _raddishShop = new RaddishShop();
    }

    private void Start()
    {
	    _raddishShop.OnLevelUp += (List<Raddish> raddishes) =>
	    {
		    CoroutineHelper.Instance.StartCoroutine(StartLevelUp());
	    };
    }

    IEnumerator StartLevelUp()
    {
	    _raddishShop.DisableShop = true;
	    yield return new WaitForSeconds(10);
	    _raddishShop.DisableShop = false;
    }
}
